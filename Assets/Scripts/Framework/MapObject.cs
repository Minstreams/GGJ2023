using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 地图上存在的基类，有体积
    /// </summary>
    public abstract class MapObject : MonoBehaviour
    {
        [Group("地图")]
        public GMapType mapType;

        public Vector2Int size = Vector2Int.one;
        public Vector2Int center = Vector2Int.zero;

        public int viewRange = 0;
        public bool isTerrain = false;

        public Vector2Int Pos => transform.position.ToGridPos();
        public virtual bool IsDynamic => false;

        protected GMap Map => Ice.Gameplay.map;
        Vector2Int? lastPos = null;
        Renderer[] rs = null;

        public bool IsOnMap { get; set; } = true;
        public bool IsVisible { get; private set; } = false;

        protected virtual void Awake()
        {
            rs = GetComponentsInChildren<Renderer>(true);
            OutSight();
            Ice.Gameplay.onSmallUpdateList.Add(this);
        }
        public void TrySetVisible(bool visible)
        {
            if (visible == IsVisible) return;

            if (visible)
            {
                IsVisible = true;
                onSight?.Invoke();
                OnSight();
            }
            else
            {
                bool canSee = false;
                ForEachUnit(u =>
                {
                    if (u.Visibility > 0) canSee = true;
                });

                if (!canSee)
                {
                    IsVisible = false;
                    OutSight();
                }
            }
        }

        public SimpleEvent onSight;
        protected virtual void OnSight()
        {
            if (rs != null)
            {
                foreach (var r in rs) r.enabled = true;
            }
        }
        protected virtual void OutSight()
        {
            if (isTerrain) return;
            if (size == Vector2Int.zero) return;
            if (rs != null)
            {
                foreach (var r in rs) r.enabled = false;
            }
        }

        public void ForEachUnit(System.Action<GMapUnit> action, Vector2Int? posOverride = null)
        {
            var pos = posOverride ?? Pos;
            for (int y = 0; y < size.y; ++y)
            {
                for (int x = 0; x < size.x; ++x)
                {
                    action(Map[pos.x + x - center.x, pos.y + y - center.y]);
                }
            }
        }
        public void ForEachViewUnit(System.Action<GMapUnit> action, Vector2Int? posOverride = null)
        {
            if (viewRange == 0) return;
            var p = posOverride ?? Pos;
            for (int y = -viewRange; y <= viewRange; ++y)
            {
                for (int x = -viewRange; x <= viewRange; ++x)
                {
                    if ((new Vector2Int(x, y)).sqrMagnitude <= viewRange * viewRange)
                    {
                        action(Map[p.x + x, p.y + y]);
                    }
                }
            }
        }
        public virtual void SmallUpdate()
        {
            if (!enabled) return;
            if (!IsOnMap) return;
            if (!isTerrain && lastPos != null && !IsVisible) return;
            var p = Pos;
            var pNormalized = Map[p].pos;
            if (lastPos == null)
            {
                lastPos = pNormalized;
                ForEachUnit(u => u.objList.Add(this));
                ForEachViewUnit(u => u.Visibility++);
                ForEachUnit(u =>
                {
                    if (u.Visibility > 0) TrySetVisible(true);
                });
                //Ice.Gameplay.map.maskTex.Apply();
            }
            else if (IsDynamic && lastPos.Value != pNormalized)
            {
                ForEachUnit(u => u.objList.Remove(this), lastPos);
                ForEachViewUnit(u => u.Visibility--, lastPos);
                ForEachUnit(u => u.objList.Add(this));
                ForEachViewUnit(u => u.Visibility++);
                lastPos = pNormalized;
                //Ice.Gameplay.map.maskTex.Apply();
            }

            if (CameraMgr.Instance != null)
            {
                var cp = CameraMgr.Instance.transform.position.ToGridPos();
                int w = Map.Width;
                int wHalf = w >> 1;
                int h = Map.Height;
                int hHalf = h >> 1;

                var pos = transform.position;
                if (cp.x - p.x > wHalf) pos.x += w;
                if (cp.x - p.x < -wHalf) pos.x -= w;
                if (cp.y - p.y > hHalf) pos.z += h;
                if (cp.y - p.y < -hHalf) pos.z -= h;

                transform.position = pos;
            }
        }
        protected virtual void OnDestroy()
        {
            Ice.Gameplay.onSmallUpdateList.Remove(this);
            if (!IsOnMap) return;
            ForEachUnit(u => u.objList.Remove(this));
            ForEachViewUnit(u => u.Visibility--);
            //Ice.Gameplay.map.maskTex.Apply();
        }
        protected virtual void OnEnable()
        {
        }
        protected virtual void OnDisable()
        {

            if (!IsOnMap) return;
            ForEachUnit(u => u.objList.Remove(this), lastPos);
            ForEachViewUnit(u => u.Visibility--, lastPos);
            //Ice.Gameplay.map.maskTex.Apply();
            IsOnMap = false;
        }

        public Color mapGizmoColor = new Color(0, 1, 0, 0.3f);
        protected virtual void OnDrawGizmos()
        {
            using var _ = new GizmosColorScope(mapGizmoColor);
            Gizmos.DrawCube(transform.position.Snap(1) + size.ToWorldPos(true) * 0.5f - new Vector3(0.5f, 0, 0.5f) - center.ToWorldPos(true), new Vector3(size.x, 1, size.y));
            if (viewRange > 0)
            {
                using (new GizmosColorScope(new Color(1, 0, 0.7f)))
                {
                    Gizmos.DrawWireSphere(transform.position, viewRange);
                }
            }
        }

        [Button]
        public void PutOnGround()
        {
            if (Physics.Raycast(new Ray(transform.position + Vector3.up * 256, Vector3.down), out var hit, 1000, 1 << 6))
            {
                transform.position = hit.point;
            }
        }

        [Button]
        public void PutOnGroundAndRotate()
        {
            if (Physics.Raycast(new Ray(transform.position + Vector3.up * 256, Vector3.down), out var hit, 1000, 1 << 6))
            {
                transform.position = hit.point;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }
}

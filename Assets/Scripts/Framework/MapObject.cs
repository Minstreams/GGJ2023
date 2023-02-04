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

        public Vector2Int Pos => transform.position.ToGridPos();

        protected GMap Map => Ice.Gameplay.map;
        Vector2Int? lastPos = null;

        public bool IsOnMap { get; set; } = true;

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
        protected virtual void LateUpdate()
        {


            if (!IsOnMap) return;
            var p = Pos;
            if (lastPos == null)
            {
                lastPos = p;
                ForEachUnit(u => { if (u.obj == null) u.obj = this; });
                ForEachViewUnit(u => u.Visibility++);
            }
            else if (lastPos.Value != p)
            {
                ForEachUnit(u => { if (u.obj == this) u.obj = null; }, lastPos);
                ForEachViewUnit(u => u.Visibility--, lastPos);
                ForEachUnit(u => { if (u.obj == null) u.obj = this; });
                ForEachViewUnit(u => u.Visibility++);
                lastPos = p;
            }
        }
        protected virtual void OnDestroy()
        {
            ForEachUnit(u => { if (u.obj == this) u.obj = null; });
            ForEachViewUnit(u => u.Visibility--);
        }
        protected virtual void OnDisable()
        {
            ForEachUnit(u => { if (u.obj == this) u.obj = null; });
            ForEachViewUnit(u => u.Visibility--);
        }

        public Color mapGizmoColor = new Color(0, 1, 0, 0.3f);
        protected virtual void OnDrawGizmos()
        {
            using var _ = new GizmosColorScope(mapGizmoColor);
            Gizmos.DrawCube(transform.position + size.ToWorldPos() * 0.5f - new Vector3(0.5f, 0, 0.5f) - center.ToWorldPos(), new Vector3(size.x, 1, size.y));
            if (viewRange > 0)
            {
                using (new GizmosColorScope(new Color(1, 0, 0.7f)))
                {
                    Gizmos.DrawWireSphere(transform.position, viewRange);
                }
            }
        }
    }
}

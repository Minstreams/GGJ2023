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

        public Vector2Int Pos => transform.position.ToGridPos();

        GMap Map => Ice.Gameplay.map;
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

        protected virtual void LateUpdate()
        {
            if (!IsOnMap) return;
            if (lastPos == null)
            {
                lastPos = Pos;
                ForEachUnit(u => u.type = mapType);
            }
            else if (lastPos.Value != Pos)
            {
                ForEachUnit(u => u.type = GMapType.Path, lastPos);
                ForEachUnit(u => u.type = mapType);
                lastPos = Pos;
            }
        }

        public Color mapGizmoColor = new Color(0, 1, 0, 0.3f);
        protected virtual void OnDrawGizmos()
        {
            using var _ = new GizmosColorScope(mapGizmoColor);
            Gizmos.DrawCube(transform.position + size.ToWorldPos() * 0.5f - center.ToWorldPos(), new Vector3(size.x, size.y, 1));
        }
    }
}

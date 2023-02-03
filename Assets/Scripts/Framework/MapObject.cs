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

        protected virtual void Start() => UpdateMap();

        [Button]
        public void UpdateMap()
        {
            var map = Ice.Gameplay.map;

            for (int y = 0; y < size.y; ++y)
            {
                for (int x = 0; x < size.x; ++x)
                {
                    map[Pos.x + x - center.x, Pos.y + y - center.y].type = mapType;
                }
            }
        }

        protected virtual Color MapGizmoColor => Color.green;
        protected virtual void OnDrawGizmosSelected()
        {
            using var _ = new GizmosColorScope(MapGizmoColor);
            Gizmos.DrawWireCube(transform.position + size.ToWorldPos() * 0.5f - center.ToWorldPos(), new Vector3(size.x, size.y, 1));
        }
    }
}

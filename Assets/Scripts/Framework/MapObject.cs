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


        protected virtual void Start() => UpdateMap();

        public void UpdateMap()
        {
            var map = Ice.Gameplay.map;

            for (int y = 0; y < size.y; ++y)
            {
                for (int x = 0; x < size.x; ++x)
                {
                    map[x - center.x, y - center.y].type = mapType;
                }
            }
        }

        protected virtual Color MapGizmoColor => Color.green;
        protected virtual void OnDrawGizmosSelected()
        {
            using var _ = new GizmosColorScope(MapGizmoColor);
            Gizmos.DrawWireCube(transform.position + new Vector3(size.x, size.y) * 0.5f - new Vector3(center.x, center.y), new Vector3(size.x, size.y, 1));
        }
    }
}

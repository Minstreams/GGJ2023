using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 放一些寻路算法？
    /// </summary>
    public static class GMapExtension
    {
        static GMap Map => Ice.Gameplay.map;
        public static Vector3 ToWorldPos(this Vector2Int gridPos)
        {
            float x = gridPos.x;
            float y = gridPos.y;

            if (Map != null && CameraMgr.Instance != null)
            {
                var cp = CameraMgr.Instance.transform.position;
                int w = Map.Width;
                int wHalf = w >> 1;
                int h = Map.Height;
                int hHalf = h >> 1;

                while (x - cp.x > wHalf) x -= w;
                while (x - cp.x < -wHalf) x += w;
                while (y - cp.z > hHalf) y -= h;
                while (y - cp.z < -hHalf) y += h;
            }

            return new Vector3(x, Map != null ? Map[gridPos].height : 0, y);
        }
        public static Vector2Int ToGridPos(this Vector3 worldPos) => new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z));
    }
}
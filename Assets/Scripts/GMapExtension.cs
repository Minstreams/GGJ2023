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
        public static Vector3 ToWorldPos(this Vector2Int gridPos) => new Vector3(gridPos.x, gridPos.y);
        public static Vector2Int ToGridPos(this Vector3 worldPos) => new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
    }
}
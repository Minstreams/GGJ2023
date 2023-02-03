using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    // 游戏运行时地图
    public class GMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GMap(int width, int height)
        {
            Width = width;
            Height = height;

            //tex = new Texture2D(width, height);
            data = new GMapUnit[width, height];
        }

        // actual data
        //Texture2D tex;
        GMapUnit[,] data = null;


        public GMapUnit this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width) x %= Width;
                if (y < 0 || y >= Height) y %= Height;
                return data[x, y];
                //return tex.GetPixel(x, y);
            }
            set
            {
                data[x, y] = value;
                //tex.SetPixel(x, y, value);
                //tex.Apply();
            }
        }
    }

    public class GMapUnit
    {
        /// <summary>
        /// 是否可通行
        /// </summary>
        public bool IsPath => type == GMapType.Path;

        public GMapType type;

        public Vector2Int pos;
    }

    public enum GMapType
    {
        Path,
        Collider,
    }
}
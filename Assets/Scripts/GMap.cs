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
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[x, y] = new GMapUnit(x, y);
                }
            }
        }

        // actual data
        //Texture2D tex;
        GMapUnit[,] data = null;


        public GMapUnit this[Vector2Int pos] => this[pos.x, pos.y];
        public GMapUnit this[int x, int y]
        {
            get
            {
                while (x < 0) x += Width;
                if (x >= Width) x %= Width;
                while (y < 0) y += Height;
                if (y >= Height) y %= Height;
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

        public GMapUnit(int x, int y)
        {
            pos = new Vector2Int(x, y);
            type = GMapType.Path;
        }
    }

    public enum GMapType
    {
        Path,
        Collider,
    }
}
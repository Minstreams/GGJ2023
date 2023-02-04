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

        public GMap(Vector2Int size)
        {
            Width = size.x;
            Height = size.y;

            //tex = new Texture2D(width, height);
            data = new GMapUnit[size.x, size.y];
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
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

        public Vector3 GetDirection(Vector3 dir)
        {
            while (dir.x > Width * 0.5f) dir.x -= Width;
            while (dir.x < -Width * 0.5f) dir.x += Width;
            while (dir.y > Height * 0.5f) dir.y -= Height;
            while (dir.y < -Height * 0.5f) dir.y += Height;
            return dir.normalized;
        }
    }

    public class GMapUnit
    {
        /// <summary>
        /// 是否可通行
        /// </summary>
        public bool IsPath(GMapType t = GMapType.Path)
        {
            return t switch
            {
                GMapType.Robot => Type == GMapType.Path || Type == GMapType.Building,
                _ => Type == GMapType.Path
            };
        }
        public GMapType Type
        {
            get
            {
                if (obj != null)
                {
                    return obj.mapType;
                }
                return GMapType.Path;
            }
        }
        //=> obj != null ? obj.mapType : GMapType.Path;

        public Vector2Int pos;
        public MapObject obj;

        public GMapUnit(int x, int y)
        {
            pos = new Vector2Int(x, y);
        }
    }

    public enum GMapType
    {
        Path,
        Building,
        Robot,
        Source,
        Enemy,
        Collider,
    }
}
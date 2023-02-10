using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            maskTex = new Texture2D(Width, Height, TextureFormat.RGBA32, false, true);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    maskTex.SetPixel(x, y, Color.black);
                }
            }
            maskTex.Apply();
            Shader.SetGlobalTexture("_GMap", maskTex);
            Shader.SetGlobalVector("_GMapVector", new Vector4(Width, Height, 0, 0));
        }

        // actual data
        //Texture2D tex;
        public GMapUnit[,] data = null;
        public Texture2D maskTex = null;

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

        public void UpdateHeight()
        {
            foreach (var u in data)
            {
                u.UpdateHeight();
            }
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
                GMapType.Robot => Type == GMapType.Path || Type == GMapType.Building || Type == GMapType.Source,
                _ => Type == GMapType.Path
            };
        }
        public GMapType Type
        {
            get
            {
                if (Obj != null)
                {
                    return Obj.mapType;
                }
                return GMapType.Path;
            }
        }
        //=> obj != null ? obj.mapType : GMapType.Path;

        public Vector2Int pos;
        public List<MapObject> objList = new List<MapObject>();
        public MapObject Obj => objList.Any() ? objList[0] : null;
        public float height;

        int _visibility;
        /// <summary>
        /// 可见度，大于0时可见，等于0不可见
        /// </summary>
        public int Visibility
        {
            get => _visibility; set
            {
                if (value != _visibility)
                {
                    if (value > 0 != _visibility > 0)
                    {
                        Ice.Gameplay.map.maskTex.SetPixel(pos.x, pos.y, value > 0 ? Color.white : Color.black);
                        //Ice.Gameplay.map.maskTex.Apply();

                        if (objList.Any())
                        {
                            foreach (var obj in objList) obj.TrySetVisible(value > 0);
                        }
                    }
                    _visibility = value;
                }
            }
        }

        public GMapUnit(int x, int y)
        {
            pos = new Vector2Int(x, y);
        }

        public void UpdateHeight()
        {
            int w = Ice.Gameplay.map.Width;
            int h = Ice.Gameplay.map.Height;

            RaycastHit hit = default;
            if (Physics.Raycast(new Ray(new Vector3(pos.x, 256, pos.y), Vector3.down), out hit, 1000, 1 << 6))
            {
                height = hit.point.y;
            }
            else if (Physics.Raycast(new Ray(new Vector3(pos.x - w, 256, pos.y), Vector3.down), out hit, 1000, 1 << 6))
            {
                height = hit.point.y;
            }
            else if (Physics.Raycast(new Ray(new Vector3(pos.x - w, 256, pos.y - h), Vector3.down), out hit, 1000, 1 << 6))
            {
                height = hit.point.y;
            }
            else if (Physics.Raycast(new Ray(new Vector3(pos.x, 256, pos.y - h), Vector3.down), out hit, 1000, 1 << 6))
            {
                height = hit.point.y;
            }
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
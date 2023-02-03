using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IceEngine;

// 游戏运行时地图
public class GMap
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public GMap(int width, int height)
    {
        Width = width;
        Height = height;

        tex = new Texture2D(width, height);
    }

    // actual data
    Texture2D tex;

    public Color this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Width) x %= Width;
            if (y < 0 || y >= Height) y %= Height;
            return tex.GetPixel(x, y);
        }
        set
        {
            tex.SetPixel(x, y, value);
            tex.Apply();
        }
    }
}

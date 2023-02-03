using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 可选择的物体
    /// </summary>
    public abstract class Selectable : MapObject
    {
        public static Selectable SelectedObject { get; private set; }

        void OnMouseDown()
        {
            Ice.Gameplay.Log("Select" + gameObject.name);
        }
    }
}

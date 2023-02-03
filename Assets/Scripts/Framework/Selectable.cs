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
        [Group("Selectable")]
        public string displayName;
        [Multiline]
        public string displayDescription;

        public List<OptionItem> options = new List<OptionItem>();

        void OnMouseDown()
        {
            Ice.Gameplay.Log("Select" + gameObject.name);
            Ice.Gameplay.SelectObject(this);
        }
    }
}

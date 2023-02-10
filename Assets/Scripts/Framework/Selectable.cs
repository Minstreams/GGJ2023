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
        [Group("功能")]
        public string displayName;
        [Multiline(10)]
        public string displayDescription;

        protected virtual string DisplayDescriptionExtra => "";
        public string DisplayDescription => displayDescription + "\n" + DisplayDescriptionExtra;

        public List<OptionItem> options = new List<OptionItem>();

        void OnMouseDown()
        {
            if (Input.mousePosition.x > Screen.width * 0.8f) return;
            if (!IsOnMap) return;
            Ice.Gameplay.Log("Select" + gameObject.name);
            Ice.Gameplay.SelectObject(this);
        }

        [IceprintPort] public SimpleEvent onSelected;
        [IceprintPort] public SimpleEvent onDiselected;
        public void Select()
        {
            onSelected?.Invoke();
        }
        public void Diselect()
        {
            onDiselected?.Invoke();
        }
    }
}

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
        [Multiline]
        public string displayDescription;

        protected virtual string DisplayDescriptionExtra => "";
        public string DisplayDescription => displayDescription + "\n" + DisplayDescriptionExtra;

        public List<OptionItem> options = new List<OptionItem>();

        void OnMouseDown()
        {
            if (!IsOnMap) return;
            Ice.Gameplay.Log("Select" + gameObject.name);
            Ice.Gameplay.SelectObject(this);
        }

        public SimpleEvent onSelected;
        public SimpleEvent onDiselected;
        public void Select()
        {
            onSelected?.Invoke();
        }
        public void Diselect()
        {
            onDiselected?.Invoke();
        }

        protected virtual void OnBecomeVisible() { }
        protected virtual void OnBecomeInvisible() { }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    [CreateAssetMenu(fileName = "OptionItem", menuName = "物品选项/默认")]
    public class OptionItem : ScriptableObject
    {
        public Sprite icon;
        public string tip;

        public float price;
        public float cd;

        public virtual void OnClick(Selectable obj) { }
    }
}

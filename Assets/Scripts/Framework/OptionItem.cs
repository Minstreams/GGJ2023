using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public abstract class OptionItem : ScriptableObject
    {
        public Sprite icon;
        public string tip;

        public int price;
        public float cd;

        public string requiredKey;

        public virtual void OnClick(Selectable obj)
        {
            // price
            if (Ice.Gameplay.Money >= price)
            {
                Ice.Gameplay.Money -= price;
                OnAct(obj);
            }
            else
            {
                // TODO: 视觉表现
            }
        }
        public abstract void OnAct(Selectable obj);
    }
}

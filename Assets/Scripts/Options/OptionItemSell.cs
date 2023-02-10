using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    [CreateAssetMenu(fileName = "OptionItem", menuName = "物品选项/售卖建筑")]
    public class OptionItemSell : OptionItem
    {
        public override string Price => (Ice.Gameplay.SelectedObject is Building b) ? b.sellPrice.ToString() : base.Price;
        public override void OnAct(Selectable obj)
        {
            if (obj is Building b)
            {
                int price = b.sellPrice;
                b.Die();
                Ice.Gameplay.Money += price;
            }
        }
    }
}

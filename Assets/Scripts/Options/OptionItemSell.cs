using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    [CreateAssetMenu(fileName = "OptionItem", menuName = "物品选项/售卖建筑")]
    public class OptionItemSell : OptionItem
    {
        public override void OnAct(Selectable obj)
        {
            if (obj is Building b)
            {
                int price = b.sellPrice;
                Destroy(b.gameObject);
                Ice.Gameplay.Money += price;
            } 
        }
    }
}

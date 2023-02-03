using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    [CreateAssetMenu(fileName = "OptionItem", menuName = "物品选项/摆放建筑")]
    public class OptionItemBuild : OptionItem
    {
        public GameObject prefabBuilding;

        public override void OnAct(Selectable obj)
        {
            Ice.Gameplay.PutBuilding(this);
        }
    }
}

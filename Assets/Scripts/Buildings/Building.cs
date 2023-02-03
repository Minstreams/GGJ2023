using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    // 建筑基类，有血，可被点击
    public class Building : MonoBehaviour
    {
        public static Building Selected { get; private set; }

        [Group("配置")]
        public float maxhp;

        public float HP { get; set; }


        void OnMouseDown()
        {
            Ice.Gameplay.Log("Select" + gameObject.name);
        }
    }
}

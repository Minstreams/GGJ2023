using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 有血条的物体
    /// </summary>
    public abstract class Hurtable : Selectable
    {
        [Group("生命")]
        public float maxHp;

        public float HP { get; set; }
    }
}

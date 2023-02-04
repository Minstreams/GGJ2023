using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 基地
    /// </summary>
    public class Basement : Hurtable
    {
        protected override void OnDie()
        {
            throw new System.NotImplementedException();
        }

        void Awake()
        {
            Ice.Gameplay.playerTargets.Add(this);
        }
    }
}

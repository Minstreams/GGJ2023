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
        public string key;
        private void Awake()
        {
            IsOnMap = false;
        }

        protected override void OnDie()
        {
            Ice.Gameplay.LockKey(key);
            FXEmitter.PlayAt(FXType.DestroyBuilding, transform.position, size: 8);
            Ice.Gameflow.SendMsg("GameOver");
        }

        protected override void OnSight()
        {
            Ice.Gameplay.playerTargets.Add(this);
            base.OnSight();
            Ice.Gameplay.UnlockKey(key);
        }
    }
}

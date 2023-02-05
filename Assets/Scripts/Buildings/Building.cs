using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    // 可建造的建筑基类，可被点击
    public abstract class Building : Hurtable
    {
        [Group("建筑")]
        public SimpleEvent onBuilt;
        public int sellPrice = 100;

        public void Build()
        {
            RestoreHP();
            FXEmitter.PlayAt(FXType.PutBuilding, transform.position);
            OnBuilt();
            onBuilt?.Invoke();
            Ice.Gameplay.playerTargets.Add(this);
        }
        protected abstract void OnBuilt();

        protected override void OnDie()
        {
            Ice.Gameplay.playerTargets.Remove(this);
            FXEmitter.PlayAt(FXType.DestroyBuilding, AimPos);
            Destroy(gameObject);
        }

        protected override void OnHurted(float delta, Hurtable attacker)
        {
            if (Houmen.godMode) return;
            base.OnHurted(delta, attacker);
        }
    }
}

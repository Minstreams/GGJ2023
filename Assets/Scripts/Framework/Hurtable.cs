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
        public Vector3 aimOffset;

        float _hp;
        public float HP
        {
            get => _hp; private set
            {
                if (value < 0)
                {
                    _hp = 0;
                    Die();
                }
                else _hp = value;
            }
        }
        public void RestoreHP()
        {
            HP = maxHp;
        }
        public void Hurt(float delta, Hurtable attacker)
        {
            if (!IsOnMap) return;
            onHurted?.Invoke(delta);
            OnHurted(delta, attacker);
            HP -= delta;
        }
        public Vector3 AimPos => transform.position + aimOffset;

        public SimpleEvent onDie;
        public FloatEvent onHurted;
        public void Die()
        {
            onDie?.Invoke();
            OnDie();
        }
        protected abstract void OnDie();
        protected virtual void OnHurted(float delta, Hurtable attacker) { }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(transform.position + aimOffset, 0.2f);
        }
    }
}

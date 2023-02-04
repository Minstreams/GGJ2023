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

        float _hp;
        public float HP
        {
            get => _hp; set
            {
                if (value < 0)
                {
                    _hp = 0;
                    Die();
                }
                else _hp = value;
            }
        }

        public SimpleEvent onDie;
        public void Die()
        {
            onDie?.Invoke();
            OnDie();
        }
        protected abstract void OnDie();
    }
}

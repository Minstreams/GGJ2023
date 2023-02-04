﻿using System.Collections;
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
            OnBuilt();
            onBuilt?.Invoke();
            Ice.Gameplay.playerTargets.Add(this);
        }
        protected abstract void OnBuilt();

        protected override void OnDie()
        {
            Ice.Gameplay.playerTargets.Remove(this);
            Destroy(gameObject);
        }
    }
}

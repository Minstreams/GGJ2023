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
        public static List<Basement> bs = new List<Basement>();

        public string key;
        private void Awake()
        {
            IsOnMap = false;
            RestoreHP();
        }

        protected override void OnDie()
        {
            Ice.Gameplay.LockKey(key);
            FXEmitter.PlayAt(FXType.DestroyBuilding, transform.position, size: 8);
            Ice.Gameflow.SendMsg("GameOver");
            IsOnMap = false;
        }

        protected override void OnSight()
        {
            bs.Add(this);
            foreach (var b in bs)
            {
                b.Disconnect();
            }

            if (bs.Count > 1)
            {
                bs[bs.Count - 1].ConnectTo(bs[0]);

                for (int i = 0; i < bs.Count - 1; i++)
                {
                    bs[i].ConnectTo(bs[i + 1]);
                }
            }

            Ice.Gameplay.playerTargets.Add(this);
            base.OnSight();
            Ice.Gameplay.UnlockKey(key);
        }

        public Transform p0;
        public LineRenderer lr;
        SimpleEvent onConnect;
        SimpleEvent onDisconnect;
        void Disconnect()
        {
            lr.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            onDisconnect?.Invoke();
        }
        void ConnectTo(Basement t)
        {
            lr.SetPositions(new Vector3[] { p0.position, t.p0.position });
            onConnect?.Invoke();
        }

        protected override void OnHurted(float delta, Hurtable attacker)
        {
            if (Houmen.godMode) return;
            base.OnHurted(delta, attacker);
        }
    }
}

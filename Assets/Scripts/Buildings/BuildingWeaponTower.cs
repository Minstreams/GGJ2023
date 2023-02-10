using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 防御塔
    /// </summary>
    public class BuildingWeaponTower : Building
    {
        [Group("防御塔")]
        public int range = 4;

        public Hurtable Target { get; private set; }

        Weapon weapon;
        protected override void OnBuilt()
        {
            StartCoroutine(_Main());
            weapon = GetComponentInChildren<Weapon>();
        }
        IEnumerator _Main()
        {
            while (true)
            {
                yield return Ice.Gameplay.inter3;

                if (Target == null || !Target.IsOnMap || Mathf.Max(Mathf.Abs(Target.Pos.x - Pos.x), Mathf.Abs(Target.Pos.y - Pos.y)) > range)
                {
                    Target = null;

                    SearchEnemies();

                    void SearchEnemies()
                    {
                        for (int d = 1; d <= range; d++)
                        {
                            for (int i = 1 - d; i <= d; i++)
                            {
                                var p = Pos;
                                if (Process(p.x - d, p.y + i)) return;
                                if (Process(p.x + d, p.y - i)) return;
                                if (Process(p.x - i, p.y - d)) return;
                                if (Process(p.x + i, p.y + d)) return;
                            }
                        }
                    }

                    bool Process(int x, int y)
                    {
                        var obj = Map[x, y].Obj;
                        if (obj is Enemy or Nest && obj.IsOnMap)
                        {
                            Target = obj as Hurtable;
                            return true;
                        }
                        return false;
                    }
                }

                if (Target == null) continue;

                if (weapon != null)
                {
                    weapon.TryFire(Target);
                }
            }
        }

        protected override void OnHurted(float delta, Hurtable attacker)
        {
            base.OnHurted(delta, attacker);
            Target = attacker;
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            using (new GizmosColorScope(new Color(1, 0.25f, 0)))
            {
                Gizmos.DrawWireCube(transform.position, Vector3.one * range * 2);
            }
        }
    }
}

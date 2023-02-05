using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IceEngine
{
    public class Enemy : Hurtable
    {
        [Group("Enemy")]
        public float speed;
        public float shootRange;

        public Hurtable Target { get; private set; }
        public Nest Parent { get; set; }

        Weapon weapon;
        private void Awake()
        {
            weapon = GetComponentInChildren<Weapon>();
        }


        List<Vector2Int> path = new List<Vector2Int>();
        public bool Go()
        {
            StartCoroutine(_Go());

            return true;
        }

        IEnumerator _Go()
        {
            while (true)
            {
                yield return 0;
                if (!Ice.Gameplay.playerTargets.Any())
                {
                    yield break;
                }

                if (Target == null || !Target.IsOnMap)
                {
                    // 随机一个建筑
                    Target = Ice.Gameplay.playerTargets[Random.Range(0, Ice.Gameplay.playerTargets.Count)];
                }

                if (!path.Any() || !Map[path[0]].IsPath(mapType))
                {
                    // 路径走完或者撞到障碍，重新寻路
                    Astar.FindingPath(Pos, Target.Pos, path, mapType, 8);
                }
                else
                {
                    transform.position += speed * Time.deltaTime * Map.GetDirection(path[0].ToWorldPos() - transform.position);
                    if (Vector3.Distance(Target.transform.position, transform.position) - Target.size.x * 0.5f < shootRange) weapon?.TryFire(Target);

                    if (Map[Pos].pos == path[0]) path.RemoveAt(0);
                }
            }
        }


        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            using (new GizmosColorScope(Color.cyan))
            {
                Vector2Int lastPos = Pos;
                foreach (var p in path)
                {
                    Gizmos.DrawLine(p.ToWorldPos() + Vector3.one * 0.5f, lastPos.ToWorldPos() + Vector3.one * 0.5f);
                    lastPos = p;
                }
            }
            using (new GizmosColorScope(Color.red))
            {
                Gizmos.DrawWireSphere(transform.position, shootRange);
            }
        }

        protected override void OnDie()
        {
            Parent.Recycle(this);
        }

        protected override void OnHurted(float delta, Hurtable attacker)
        {
            path.Clear();
            Target = attacker;
        }
    }
}

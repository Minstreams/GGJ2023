using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 资源采集机器人
    /// </summary>
    public class SCV : Hurtable
    {
        public Source Target { get; private set; }
        public float Speed { get; private set; }
        public int Money { get; private set; }
        public BuildingSightTower Tower { get; set; }
        public override bool IsDynamic => true;

        List<Vector2Int> path = new List<Vector2Int>();
        public bool Go(Source target, float speed)
        {
            if (Astar.FindingPath(Pos, target.Pos, path, mapType, 64))
            {
                Target = target;
                Speed = speed;
                Money = 0;

                target.CurSCV = this;

                StartCoroutine(_Go());

                return true;
            }
            else
            {
                Tower.RecycleSCV(this);

                return false;
            }
        }

        IEnumerator _Go()
        {
            if (!path.Any())
            {
                throw new System.Exception("机器人没有找到路！");
            }

            int pathId = 0;
            while (true)
            {
                yield return 0;
                if (Map[Pos].pos == path[pathId]) ++pathId;

                if (pathId >= path.Count)
                {
                    // 到了，收集到资源
                    Money += Target.money;
                    Destroy(Target.gameObject);
                    --pathId;
                    break;
                }

                transform.position += Map.GetDirection(path[pathId].ToWorldPos() - transform.position) * Time.deltaTime * Speed;
            }

            while (true)
            {
                yield return 0;
                if (Map[Pos].pos == path[pathId]) --pathId;

                if (pathId < 0)
                {
                    // 回家了
                    Ice.Gameplay.Money += Money;
                    Tower.RecycleSCV(this);
                    break;
                }

                transform.position += Map.GetDirection(path[pathId].ToWorldPos() - transform.position) * Time.deltaTime * Speed;
            }
        }

        protected override void OnDie()
        {
            Tower.RecycleSCV(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Ice.Gameplay.playerTargets.Add(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Ice.Gameplay.playerTargets.Remove(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 敌人巢穴
    /// </summary>
    public class Nest : Hurtable
    {
        [Group("Nest")]
        public float interval = 1;
        public Transform enemyRoot;
        [Group("Enemy")]
        public GameObject prefabEnemy;

        void Start()
        {
            RestoreHP();
        }

        protected override void OnSight()
        {
            base.OnSight();
            StartCoroutine(_Main());
        }

        protected override void OutSight()
        {
            StopAllCoroutines();
            base.OutSight();
        }

        IEnumerator _Main()
        {
            float t = interval;
            while (true)
            {
                yield return 0;
                t -= Time.deltaTime;

                if (t < 0)
                {
                    t += interval;

                    // 派一个敌人出来
                    var e = ProduceEnemy();
                    e.Go();
                }
            }
        }

        Stack<Enemy> emyStack = new Stack<Enemy>();
        public Enemy ProduceEnemy()
        {
            Enemy e = null;
            if (emyStack.Any())
            {
                e = emyStack.Pop();
                e.gameObject.SetActive(true);
            }
            else
            {
                var go = Instantiate(prefabEnemy, enemyRoot);
                e = go.GetComponent<Enemy>();
                e.Parent = this;
            }
            e.transform.position = enemyRoot.position;
            e.IsOnMap = true;
            e.RestoreHP();
            return e;
        }

        public void Recycle(Enemy e)
        {
            e.gameObject.SetActive(false);
            emyStack.Push(e);
        }

        protected override void OnDie()
        {
            Destroy(gameObject);
        }
    }
}

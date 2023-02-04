using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 资源塔
    /// </summary>
    public class BuildingSourceTower : Building
    {
        [Group("资源塔")]
        public int range = 5;
        public float interval = 1;
        public Transform scvRoot;
        [Group("SCV")]
        public float speed = 1;

        protected override void OnBuilt()
        {
            StartCoroutine(_Main());
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
                    // 查找周围的资源，并派出机器人采集

                    SearchSources();

                    void SearchSources()
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
                        if (Map[x, y].obj is Source s && s.CurSCV == null)
                        {
                            // 派出机器人逻辑
                            var scv = ProduceSCV();
                            return scv.Go(s, speed);
                        }
                        return false;
                    }
                }
            }
        }

        Stack<SCV> scvStack = new Stack<SCV>();
        public SCV ProduceSCV()
        {
            SCV scv = null;

            if (scvStack.Any())
            {
                scv = scvStack.Pop();
                scv.gameObject.SetActive(true);
            }
            else
            {
                var go = Instantiate(Ice.Gameplay.Setting.prefabScv, scvRoot);
                scv = go.GetComponent<SCV>();
                scv.Tower = this;
            }

            scv.transform.position = scvRoot.position;
            scv.IsOnMap = true;
            return scv;
        }
        public void RecycleSCV(SCV scv)
        {
            Ice.Gameplay.playerTargets.Remove(scv);
            scv.gameObject.SetActive(false);
            scv.IsOnMap = false;
            scvStack.Push(scv);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            using (new GizmosColorScope(new Color(1, 0.75f, 0)))
            {
                Gizmos.DrawWireCube(transform.position, Vector3.one * range * 2);
            }
        }
    }
}

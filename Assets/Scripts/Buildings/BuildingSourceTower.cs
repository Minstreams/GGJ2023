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
        public float grassInterval = 0.5f;
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

                    if (!SearchSources())
                    {
                        StartCoroutine(_Grass());
                        yield break;
                    }

                    bool SearchSources()
                    {
                        for (int d = 1; d <= range; d++)
                        {
                            for (int i = 1 - d; i <= d; i++)
                            {
                                var p = Map[Pos].pos;
                                if (Process(p.x - d, p.y + i)) return true;
                                if (Process(p.x + d, p.y - i)) return true;
                                if (Process(p.x - i, p.y - d)) return true;
                                if (Process(p.x + i, p.y + d)) return true;
                            }
                        }
                        return false;
                    }

                    bool Process(int x, int y)
                    {
                        if (Map[x, y].Obj is Source s && s.CurSCV == null)
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

        IEnumerator _Grass()
        {
            while (true)
            {
                yield return Ice.Gameplay.inter;
                if (scvSet.Count == 0) break;
            }

            for (int d = 1; d <= range; d++)
            {
                yield return new WaitForSeconds(grassInterval);
                for (int i = 1 - d; i <= d; i++)
                {
                    var p = Pos;
                    Process(p.x - d, p.y + i);
                    Process(p.x + d, p.y - i);
                    Process(p.x - i, p.y - d);
                    Process(p.x + i, p.y + d);
                }
            }

            void Process(int x, int y)
            {
                if (Vector2Int.Distance(new Vector2Int(x, y), Pos) > range) return;
                if (Map[x, y].Obj is null or SCV)
                {
                    var go = GameObject.Instantiate(Ice.Gameplay.Setting.prefabGrass);
                    go.transform.position = (new Vector2Int(x, y)).ToWorldPos();
                }
            }
        }

        Stack<SCV> scvStack = new Stack<SCV>();
        HashSet<SCV> scvSet = new HashSet<SCV>();
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
                //scv.Tower = this;
            }

            scvSet.Add(scv);
            scv.transform.position = scvRoot.position;
            scv.IsOnMap = true;
            scv.RestoreHP();
            return scv;
        }
        public void RecycleSCV(SCV scv)
        {
            scvSet.Remove(scv);
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IceEngine
{
    public class BuildingSightTower : Building
    {
        public static HashSet<BuildingSightTower> towerSet = new HashSet<BuildingSightTower>();
        public static void ReconnectAll()
        {
            foreach (var t in towerSet)
            {
                t.Reconnect();
            }
        }

        public LineRenderer lr;
        public Transform p0;

        protected override void OnBuilt()
        {
            towerSet.Add(this);
            ReconnectAll();
            StartCoroutine(_Main());
        }

        public BuildingSightTower ConnenctedTower { get; set; }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            towerSet.Remove(this);
            ReconnectAll();
        }

        public void Reconnect()
        {
            Disconnect();
            float minDis = float.MaxValue;
            BuildingSightTower minT = null;
            foreach (var t in towerSet)
            {
                if (t == this) continue;
                var d = Vector3.Distance(transform.position, t.transform.position);
                if (d < minDis && t.ConnenctedTower != this)
                {
                    minDis = d;
                    minT = t;
                }
            }

            if (minT != null) ConnectToTower(minT);
        }

        SimpleEvent onConnect;
        SimpleEvent onDisconnect;
        void Disconnect()
        {
            ConnenctedTower = null;
            lr.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            onDisconnect?.Invoke();
        }
        void ConnectToTower(BuildingSightTower t)
        {
            ConnenctedTower = t;
            lr.SetPositions(new Vector3[] { p0.position, t.p0.position });
            onConnect?.Invoke();
        }


        #region Old
        [Group("资源塔")]
        public int range = 5;
        public float interval = 1;
        public Transform scvRoot;
        public float grassInterval = 0.5f;
        [Group("SCV")]
        public float speed = 1;

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
                var obj = Map[x, y].Obj;
                if (obj is null or SCV)
                {
                    var go = GameObject.Instantiate(Ice.Gameplay.Setting.prefabGrass);
                    go.transform.position = (new Vector2Int(x, y)).ToWorldPos();
                }
                else if (obj is HintTrigger ht)
                {
                    ht.Hint();
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
                scv.Tower = this;
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
        #endregion
    }
}

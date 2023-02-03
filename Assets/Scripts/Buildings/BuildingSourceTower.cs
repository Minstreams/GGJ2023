using System.Collections;
using System.Collections.Generic;
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
                            for (int i = 0; i <= d; i++)
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
                        if (Map[x, y].obj is Source s)
                        {
                            // 派出机器人逻辑
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using IceEngine;
using UnityEngine;

namespace Ice
{
    public sealed class Gameplay : IceEngine.Framework.IceSystem<IceEngine.Internal.SettingGameplay>
    {
        static void Awake()
        {
            map = new GMap(Setting.mapSize);
            Log("Awake");
        }
        static void Start()
        {
            Log("Start");
            Money = Setting.initMoney;
            StartCoroutine(SmallUpdate());
        }

        public static WaitForSeconds inter = new WaitForSeconds(1);
        public static WaitForSeconds inter2 = new WaitForSeconds(0.5f);
        public static WaitForSeconds inter3 = new WaitForSeconds(0.1f);
        public static List<MapObject> onSmallUpdateList = new List<MapObject>();
        static int iter = 0;
        static int maxIter = 256;
        static IEnumerator SmallUpdate()
        {
            while (true)
            {
                yield return inter2;
                int count = onSmallUpdateList.Count;
                if (count > 0)
                {
                    int max = Mathf.Max(maxIter, count);
                    for (int i = 0; i < max; i++)
                    {
                        if (iter >= onSmallUpdateList.Count) iter = 0;
                        onSmallUpdateList[iter].SmallUpdate();
                        iter++;
                    }
                }
                map.maskTex.Apply();
            }
        }

        #region 玩家数据
        static int _money;
        public static int Money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
                PanelStatus.UpdateStatus();
            }
        }

        public static List<Hurtable> playerTargets = new List<Hurtable>();
        #endregion

        public static GMap map;

        #region 选择
        public static Selectable SelectedObject { get; set; }
        public static void SelectObject(Selectable obj)
        {
            if (SelectedObject != null) SelectedObject.Diselect();
            SelectedObject = obj;
            if (obj != null) obj.Select();
            PanelSelectedObject.UpdateContent();
        }
        #endregion

        #region 放置建筑
        static bool isPuttingBuilding = false;
        public static void PutBuilding(OptionItemBuild item)
        {
            if (isPuttingBuilding) return;

            StartCoroutine(_PutBuilding(item));
        }
        static IEnumerator _PutBuilding(OptionItemBuild item)
        {
            isPuttingBuilding = true;
            var building = GameObject.Instantiate(item.prefabBuilding);
            var mapComp = building.GetComponent<Building>();
            mapComp.IsOnMap = false;
            mapComp.Select();

            while (true)
            {
                yield return 0;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 1000, 1 << 6 /*Terrain*/))
                {
                    var pos = hit.point.ToGridPos();

                    building.transform.position = new Vector3(pos.x, map[pos].height, pos.y);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    bool canPut = true;
                    mapComp.ForEachUnit(u =>
                    {
                        if (!u.IsPath() || u.Visibility == 0) canPut = false;
                    });

                    if (canPut)
                    {
                        mapComp.IsOnMap = true;
                        mapComp.Diselect();
                        mapComp.Build();
                        break;
                    }
                    else
                    {
                        // TODO:视觉表现
                        mapComp.onHurted?.Invoke(0);
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Money += item.price;
                    GameObject.Destroy(building);
                    break;
                }

                isPuttingBuilding = false;
            }
        }
        #endregion

        #region 流程解锁逻辑
        public static HashSet<string> unlockedKeys = new HashSet<string>();
        public static bool HasKey(string key) => unlockedKeys.Contains(key);
        public static bool UnlockKey(string key) => unlockedKeys.Add(key);
        public static bool LockKey(string key) => unlockedKeys.Remove(key);
        #endregion
    }
}
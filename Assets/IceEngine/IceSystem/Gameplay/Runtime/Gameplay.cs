using System.Collections;
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

        #region 玩家数据
        // TODO: 存档问题
        #endregion

        public static GMap map;

        #region 选择
        public static Selectable SelectedObject { get; set; }
        public static void SelectObject(Selectable obj)
        {
            SelectedObject = obj;
            PanelSelectedObject.UpdateContent();
        }
        #endregion

        #region 放置建筑
        public static void PutBuilding(OptionItemBuild item)
        {
            StartCoroutine(_PutBuilding(item));
        }
        static IEnumerator _PutBuilding(OptionItemBuild item)
        {
            var building = GameObject.Instantiate(item.prefabBuilding);
            var mapComp = building.GetComponent<Building>();
            mapComp.IsOnMap = false;

            while (true)
            {
                yield return 0;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var pos = ray.origin - ray.direction * ray.origin.z / ray.direction.z;

                pos.x = Mathf.Round(pos.x);
                pos.y = Mathf.Round(pos.y);
                building.transform.position = pos;

                if (Input.GetMouseButtonDown(0))
                {
                    bool canPut = true;
                    mapComp.ForEachUnit(u =>
                    {
                        if (!u.IsPath) canPut = false;
                    });

                    if (canPut)
                    {
                        mapComp.IsOnMap = true;
                        break;
                    }
                    else
                    {
                        // TODO:视觉表现
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    // TODO: 退钱
                    GameObject.Destroy(building);
                    break;
                }
            }
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class Houmen : MonoBehaviour
    {
        public GameObject miwu;
        public static bool godMode = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Q 一键解锁
                Ice.Gameplay.UnlockKey("1");
                Ice.Gameplay.UnlockKey("2");
                Ice.Gameplay.UnlockKey("3");
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                // G 无敌模式
                godMode = !godMode;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // M 加钱
                Ice.Gameplay.Money += 10000;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                // R 复活
                PanelSelectedObject.Instance.gameObject.SetActive(true);
                PanelStatus.Instance.gameObject.SetActive(true);
                PanelGameOver.Instance.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                // W开关迷雾
                miwu.SetActive(!miwu.activeSelf);
            }
        }
    }
}

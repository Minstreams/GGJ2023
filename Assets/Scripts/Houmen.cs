using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class Houmen : MonoBehaviour
    {
        public static bool godMode = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Ice.Gameplay.UnlockKey("1");
                Ice.Gameplay.UnlockKey("2");
                Ice.Gameplay.UnlockKey("3");
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                godMode = !godMode;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                Ice.Gameplay.Money += 10000;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                PanelSelectedObject.Instance.gameObject.SetActive(true);
                PanelStatus.Instance.gameObject.SetActive(true);
                PanelGameOver.Instance.gameObject.SetActive(false);
            }
        }
    }
}

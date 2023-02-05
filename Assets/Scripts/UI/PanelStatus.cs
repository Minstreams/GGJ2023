using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IceEngine
{
    public class PanelStatus : MonoBehaviour
    {
        public static PanelStatus Instance { get; private set; }
        void Awake()
        {
            Instance = this;
            UpdateStatus();
            gameObject.SetActive(false);
        }

        public Text tMoney;

        public static void UpdateStatus() => Instance?._UpdateStatus();
        public void _UpdateStatus()
        {
            tMoney.text = Ice.Gameplay.Money.ToString();
        }
    }
}

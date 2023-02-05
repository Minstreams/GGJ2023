using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IceEngine
{
    public class PanelGameStart : MonoBehaviour
    {
        public Basement b1;
        public Basement b2;
        public Basement b3;

        public Button bb;

        int index = 0;

        [IceprintPort]
        public void Choose1()
        {
            index = 1;
            bb.interactable = true;
        }
        [IceprintPort]
        public void Choose2()
        {
            index = 2;
            bb.interactable = true;
        }
        [IceprintPort]
        public void Choose3()
        {
            index = 3;
            bb.interactable = true;
        }
        [IceprintPort]
        public void Enter()
        {
            if (index == 0) return;
            switch (index)
            {
                case 1:
                    b1.IsOnMap = true;
                    CameraMgr.Instance.focusRoot.position = b1.transform.position;
                    break;
                case 2:
                    b2.IsOnMap = true;
                    CameraMgr.Instance.focusRoot.position = b2.transform.position;
                    break;
                case 3:
                    b3.IsOnMap = true;
                    CameraMgr.Instance.focusRoot.position = b3.transform.position;
                    break;
            }
            Ice.Gameflow.SendMsg("Start");
            gameObject.SetActive(false);
        }
    }
}

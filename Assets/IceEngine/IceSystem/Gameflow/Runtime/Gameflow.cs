using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IceEngine;

namespace Ice
{
    public sealed class Gameflow : IceEngine.Framework.IceSystem<IceEngine.Internal.SettingGameflow>
    {
        public static System.Action tempAct;

        static void Start()
        {
            Log("全局状态机启动");
            StartCoroutine(_StartMenu());
        }

        static IEnumerator _StartMenu()
        {
            // Enter

            while (true)
            {
                yield return null;
                // Execute
                if (GetMsg("Start")) break;
            }

            // Exit
            StartCoroutine(_MainMap());
        }

        static IEnumerator _MainMap()
        {
            // Enter
            yield return PanelHint._ShowText("你醒了…");
            yield return PanelHint._ShowText("2057年，人工智能ICE通过图灵测试，神经网络延伸全球");
            yield return PanelHint._ShowText("2060年，全球地区爆发呼吸道传染病，感染者具有强攻击性");
            yield return PanelHint._ShowText("2062年7月，ICE启动了针对人类的种族灭绝计划“创世者”");
            yield return PanelHint._ShowText("至此，全球爆发智械危机…");
            yield return PanelHint._ShowText("2140年，人类最后的地表活动轨迹消失了…");
            yield return PanelHint._ShowText("你是人工智能Island1011，你的使命是");
            yield return PanelHint._ShowText("<size=64>找到引发这一切的起源</size>", 8);

            tempAct?.Invoke();
            PanelSelectedObject.Instance.gameObject.SetActive(true);
            PanelStatus.Instance.gameObject.SetActive(true);

            yield return new WaitForSeconds(3);
            yield return PanelHint._ShowText("还有两处遗迹未被定位");
            yield return PanelHint._ShowText("你需要搭建一座生态站，它是你的网络神经触手");

            while (true)
            {
                yield return null;
                // Execute
                if (GetMsg("GameOver")) break;
            }

            // Exit
            PanelSelectedObject.Instance.gameObject.SetActive(false);
            PanelStatus.Instance.gameObject.SetActive(false);
            PanelGameOver.Instance.gameObject.SetActive(true);
        }

        #region 消息机制
        static string curMsg = null;
        static bool GetMsg(string msg)
        {
            if (curMsg == msg)
            {
                curMsg = null;
                return true;
            }
            return false;
        }
        public static void SendMsg(string msg)
        {
            Log($"Msg received - {msg}");
            curMsg = msg;
        }
        #endregion
    }
}
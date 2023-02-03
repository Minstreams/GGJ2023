﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ice
{
    public sealed class Gameflow : IceEngine.Framework.IceSystem<IceEngine.Internal.SettingGameflow>
    {
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
            Level.LoadLevel("MainMap");

            while (true)
            {
                yield return null;
                // Execute
            }

            // Exit
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
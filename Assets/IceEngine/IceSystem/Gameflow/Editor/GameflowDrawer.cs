using System;

using IceEngine;
using static IceEditor.IceGUI;
using static IceEditor.IceGUIAuto;
using Sys = Ice.Gameflow;
using SysSetting = IceEngine.Internal.SettingGameflow;

namespace IceEditor.Internal
{
    internal sealed class GameflowDrawer : Framework.IceSystemDrawer<Sys, SysSetting>
    {
        public override void OnToolBoxGUI()
        {
            Label("游戏流程");
            Label("是一个全局状态机");
        }
    }
}
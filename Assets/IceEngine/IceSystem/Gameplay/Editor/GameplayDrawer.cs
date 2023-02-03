using System;

using IceEngine;
using static IceEditor.IceGUI;
using static IceEditor.IceGUIAuto;
using Sys = Ice.Gameplay;
using SysSetting = IceEngine.Internal.SettingGameplay;

namespace IceEditor.Internal
{
    internal sealed class GameplayDrawer : Framework.IceSystemDrawer<Sys, SysSetting>
    {
        public override void OnToolBoxGUI()
        {
            Label("Gameplay");
        }
    }
}
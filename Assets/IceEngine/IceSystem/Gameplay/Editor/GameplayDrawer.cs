using System;

using IceEngine;
using UnityEditor;
using UnityEngine;
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

    public static class GameplayTools
    {
        [MenuItem("一键落地 #D")]
        public static void PutOnGround()
        {
            foreach (var o in Selection.objects)
            {
                if (o is GameObject g)
                {
                    foreach (var mo in g.GetComponents<MapObject>())
                    {
                        mo.PutOnGround();
                    }
                }
            }
        }
        [MenuItem("一键落地 _#D")]
        public static void PutOnGroundAndRotate()
        {
            foreach (var o in Selection.objects)
            {
                if (o is GameObject g)
                {
                    foreach (var mo in g.GetComponents<MapObject>())
                    {
                        mo.PutOnGroundAndRotate();
                    }
                }
            }
        }
    }
}
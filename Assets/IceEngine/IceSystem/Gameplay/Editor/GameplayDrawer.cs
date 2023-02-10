using System;
using System.Collections.Generic;
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
        [MenuItem("Tools/一键落地 #D")]
        public static void PutOnGround()
        {
            foreach (var o in Selection.objects)
            {
                if (o is GameObject g)
                {
                    foreach (var mo in g.GetComponentsInChildren<MapObject>())
                    {
                        mo.PutOnGround();
                    }
                }
            }
        }
        [MenuItem("Tools/一键落地旋转 %#D")]
        public static void PutOnGroundAndRotate()
        {
            foreach (var o in Selection.objects)
            {
                if (o is GameObject g)
                {
                    foreach (var mo in g.GetComponentsInChildren<MapObject>())
                    {
                        mo.PutOnGroundAndRotate();
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class Hinter : MonoBehaviour
    {
        public string key;
        public string hint;
        public bool forbidFocus = false;
        [IceprintPort]
        public void TriggerHint() => TriggerHint(key);
        [IceprintPort]
        public void TriggerHint(string key)
        {
            if (PanelHint.TriggerHint(key) && !forbidFocus) CameraMgr.Instance.Focus(transform.position);
        }
        [IceprintPort]
        public void ShowHint() => ShowHint(hint);
        [IceprintPort]
        public void ShowHint(string hint)
        {
            PanelHint.ShowText(hint);
            if (!forbidFocus) CameraMgr.Instance.Focus(transform.position);
        }
    }
}

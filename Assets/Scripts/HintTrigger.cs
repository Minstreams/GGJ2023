using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class HintTrigger : Selectable
    {
        [Group("Hint"), Multiline(10)]
        public string hint;
        public bool IsHinted { get; private set; } = false;
        public SimpleEvent onHinted;

        protected override string DisplayDescriptionExtra => IsHinted ? hint : "也许有什么办法可以激活它。";
        public void Hint()
        {
            if (IsHinted) return;
            IsHinted = true;
            PanelHint.ShowText(hint, 10);
            CameraMgr.Instance.Focus(transform.position + Vector3.up * 4);
            onHinted?.Invoke();
        }
    }
}

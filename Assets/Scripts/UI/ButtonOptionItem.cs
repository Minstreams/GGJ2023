using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IceEngine
{
    public class ButtonOptionItem : MonoBehaviour
    {
        [Group("组件")]
        public Image icon;
        public Text text;

        OptionItem item;
        void Start()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);
        }
        public void Init(OptionItem item, int index)
        {
            this.item = item;
            icon.sprite = item.icon;
            text.text = $"{item.tip}\n{item.Price}";

            var rect = GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, index * 144);
        }

        void OnClick()
        {
            item.OnClick(Ice.Gameplay.SelectedObject);
        }
    }
}

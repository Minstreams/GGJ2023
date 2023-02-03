using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace IceEngine
{
    public class PanelSelectedObject : MonoBehaviour
    {
        #region Static
        static PanelSelectedObject Instance { get; set; }
        void Awake() => Instance = this;
        public static void UpdateContent() => Instance._UpdateContent();
        #endregion

        [Group("组件")]
        public Text tName;
        public Text tDescription;
        public Transform optionPanel;

        [Group("Prefab")]
        public GameObject prefabOptionItem;

        List<GameObject> optionObjs = new List<GameObject>();
        public void _UpdateContent()
        {
            foreach (var o in optionObjs)
            {
                Destroy(o);
            }
            optionObjs.Clear();

            var obj = Ice.Gameplay.SelectedObject;

            tName.text = obj.displayName;
            tDescription.text = obj.displayDescription;

            for (int i = 0; i < obj.options.Count; i++)
            {
                OptionItem item = obj.options[i];
                var option = Instantiate(prefabOptionItem, optionPanel);
                var comp = option.GetComponent<ButtonOptionItem>();
                comp.Init(item, i);
                optionObjs.Add(option);
            }
        }
    }
}

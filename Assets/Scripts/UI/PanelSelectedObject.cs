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
            tDescription.text = obj.DisplayDescription;

            for (int i = 0; i < obj.options.Count; i++)
            {
                OptionItem item = obj.options[i];
                var option = Instantiate(prefabOptionItem, optionPanel);
                var comp = option.GetComponent<ButtonOptionItem>();
                comp.Init(item, i);
                optionObjs.Add(option);
            }
        }

        //private void OnDrawGizmos()
        //{
        //    var map = Ice.Gameplay.map;
        //    if (map == null) return;

        //    for (int x = 0; x < map.Width; x++)
        //    {
        //        for (int y = 0; y < map.Height; y++)
        //        {
        //            switch (map[x, y].Type)
        //            {
        //                case GMapType.Collider:
        //                    using (new GizmosColorScope(new Color(1, 0, 0, 0.5f)))
        //                    {
        //                        Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
        //                    }
        //                    break;
        //                case GMapType.Robot:
        //                    using (new GizmosColorScope(new Color(1, 0.75f, 0, 0.5f)))
        //                    {
        //                        Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
        //                    }
        //                    break;
        //                case GMapType.Building:
        //                    using (new GizmosColorScope(new Color(0, 0.75f, 1, 0.5f)))
        //                    {
        //                        Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
        //                    }
        //                    break;
        //                case GMapType.Source:
        //                    using (new GizmosColorScope(new Color(0, 1, 1, 0.5f)))
        //                    {
        //                        Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
        //                    }
        //                    break;
        //                case GMapType.Path:
        //                default:
        //                    using (new GizmosColorScope(Color.gray))
        //                    {
        //                        Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //}
    }
}

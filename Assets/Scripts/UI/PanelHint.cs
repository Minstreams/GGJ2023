using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace IceEngine
{
    public class PanelHint : MonoBehaviour
    {
        public static PanelHint Instance { get; private set; }

        Color backColor;
        void Awake()
        {
            Instance = this;
            textObj.color = Color.clear;
            backColor = back.color;
            back.color = Color.clear;
        }

        public Text textObj;
        public Image back;
        public AnimationCurve curve;
        public float time;

        public static void ShowText(string text) => Instance.DisplayText(text);
        public static void ShowText(string text, float time) => Instance.DisplayText(text, time);
        public static IEnumerator _ShowText(string text) => Instance._Display(text, Instance.time);
        public static IEnumerator _ShowText(string text, float time) => Instance._Display(text, time);

        public void DisplayText(string text)
        {
            StartCoroutine(_Display(text, time));
        }
        public void DisplayText(string text, float time)
        {
            StartCoroutine(_Display(text, time));
        }

        public IEnumerator _Display(string text, float time)
        {
            textObj.text = text;

            float t = 0;

            while (t < 1)
            {
                yield return 0;
                if (Input.GetKeyDown(KeyCode.Escape)) t = 1;
                t += Time.deltaTime / time;
                textObj.color = Color.white * curve.Evaluate(t);
                back.color = backColor * curve.Evaluate(t);
            }

            textObj.color = Color.clear;
            back.color = Color.clear;
        }

        [System.Serializable]
        public class HintUnit
        {
            public int count = 1;
            public string hint;
        }
        public IceDictionary<string, HintUnit> hintMap = new IceDictionary<string, HintUnit>();
        Dictionary<string, int> hintRec = new Dictionary<string, int>();
        public static bool TriggerHint(string key) => Instance._TriggerHint(key);
        public bool _TriggerHint(string key)
        {
            if (key.IsNullOrWhiteSpace()) return false;
            if (!hintMap.ContainsKey(key)) return false;
            if (hintRec.TryGetValue(key, out var count))
            {
                hintRec[key] = count + 1;
            }
            else
            {
                hintRec[key] = 1;
            }

            var u = hintMap[key];
            if (hintRec[key] == u.count)
            {
                ShowText(u.hint);
                return true;
            }
            return false;
        }
    }
}

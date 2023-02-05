using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class MaterialOperator : MonoBehaviour
    {
        [Group("基础")]
        public string keyword;
        public float time;
        public AnimationCurve curve;

        Renderer r;
        void Awake() => r = GetComponent<Renderer>();

        [Group("Float")]
        public float defaultVal;
        public float targetVal;
        [Button]
        [IceprintPort]
        public void SetFloat() => SetFloat(targetVal);
        [IceprintPort]
        public void SetFloat(float val)
        {
            StopAllCoroutines();
            StartCoroutine(_SetFloat(val));
        }
        IEnumerator _SetFloat(float val)
        {
            float t = 0;

            while (t < 1)
            {
                yield return 0;
                t += Time.deltaTime / time;
                r.material.SetFloat(keyword, Mathf.Lerp(defaultVal, val, curve.Evaluate(t)));
            }

            r.material.SetFloat(keyword, defaultVal);
        }

        [Group("Color")]
        [ColorUsage(false, true)] public Color targetColor;

        Color? defaultColor;
        [Button]
        [IceprintPort]
        public void SetColor() => SetColor(targetColor);
        [IceprintPort]
        public void SetColor(Color val)
        {
            if (defaultColor == null) defaultColor = r.sharedMaterial.GetColor(keyword);

            StopAllCoroutines();
            StartCoroutine(_SetColor(val));
        }
        IEnumerator _SetColor(Color val)
        {
            float t = 0;

            while (t < 1)
            {
                yield return 0;
                t += Time.deltaTime / time;
                r.material.SetColor(keyword, Color.Lerp(defaultColor.Value, val, curve.Evaluate(t)));
            }

            r.material.SetColor(keyword, defaultColor.Value);
        }
    }
}

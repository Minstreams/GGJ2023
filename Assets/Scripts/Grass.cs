using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class Grass : MonoBehaviour
    {
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public float time = 1;
        public Vector2 scaleRange = Vector2.one;
        public float posRange = 0.5f;
        float scale = 1;
        void Start()
        {
            transform.localRotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
            transform.position += new Vector3(Random.Range(-1.0f, 1.0f) * posRange, 0, Random.Range(-1.0f, 1.0f) * posRange);
            scale = Random.Range(scaleRange.x, scaleRange.y);
            StartCoroutine(_Grow());
        }

        IEnumerator _Grow()
        {
            float t = 0;

            while (t < 1)
            {
                yield return 0;
                t += Time.deltaTime / time;
                transform.localScale = Vector3.one * curve.Evaluate(t) * scale;
            }
            transform.localScale = Vector3.one * scale;
        }

    }
}

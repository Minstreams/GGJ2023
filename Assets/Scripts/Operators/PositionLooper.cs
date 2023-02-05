using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class PositionLooper : MonoBehaviour
    {
        public AnimationCurve xCurve;
        public AnimationCurve yCurve;
        public AnimationCurve zCurve;

        public float intensity = 1;
        public float time;
        public Vector2 offsetRange;

        Vector3 originPos;
        float t;

        void Start()
        {
            originPos = transform.localPosition;
            t = Random.Range(offsetRange.x, offsetRange.y);
        }

        void Update()
        {
            t += Time.deltaTime / time;
            if (t > 1) t--;

            transform.localPosition = originPos + new Vector3(
                xCurve.Evaluate(t),
                yCurve.Evaluate(t),
                zCurve.Evaluate(t));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class LineOperator : MonoBehaviour
    {
        public Transform p1;
        public Transform p2;

        public float time;
        public float width = 1;
        public AnimationCurve widthCurve;

        LineRenderer lr;
        void Awake()
        {
            lr = GetComponent<LineRenderer>();
        }

        [Button]
        public void Boom()
        {
            lr.SetPositions(new Vector3[] { p1.position, p2.position });
            StopAllCoroutines();
            StartCoroutine(_Boom());
        }

        IEnumerator _Boom()
        {
            float t = 0;
            while (t < 1)
            {
                yield return 0;
                t += Time.deltaTime / time;
                lr.startWidth = width * widthCurve.Evaluate(t);
            }
            lr.startWidth = 0;
        }
    }
}

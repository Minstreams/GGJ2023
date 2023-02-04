using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class LineOperator : MonoBehaviour
    {
        public Transform p1;
        public Transform p2;

        LineRenderer lr;
        void Awake()
        {
            lr = GetComponent<LineRenderer>();
        }

        public void Boom()
        {
            lr.SetPositions(new Vector3[] { p1.position, p2.position });
        }
    }
}

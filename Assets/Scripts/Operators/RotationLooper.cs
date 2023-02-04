using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class RotationLooper : MonoBehaviour
    {
        public Vector3 euler;

        void Update()
        {
            transform.Rotate(euler, Space.World);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class BuildingBase : Building
    {

        SpriteRenderer sr;
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

    }
}

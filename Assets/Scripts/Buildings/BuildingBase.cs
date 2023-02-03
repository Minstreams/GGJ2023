using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class BuildingBase : Building
    {

        SpriteRenderer sr;
        protected override void Start()
        {
            base.Start();
            sr = GetComponent<SpriteRenderer>();
        }

    }
}

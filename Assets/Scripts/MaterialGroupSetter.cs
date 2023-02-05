using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class MaterialGroupSetter : MonoBehaviour
    {
        MaterialOperator[] operators;

        [ColorUsage(false, true)] public Color targetColor;

        void Start()
        {
            operators = GetComponentsInChildren<MaterialOperator>();
        }

        [Button]
        public void SetColor()
        {
            if (operators == null) return;

            foreach (var op in operators) op.SetColor();
        }

        [Button]
        public void SetColorOverride()
        {
            if (operators == null) return;

            foreach (var op in operators) op.SetColor(targetColor);
        }

    }
}

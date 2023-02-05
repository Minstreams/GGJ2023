using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class FXEmitter : MonoBehaviour
    {
        public IceEnumMap<FXType, SimpleLinker> particles = new();

        public static FXEmitter Instance { get; private set; }
        void Awake() => Instance = this;
        void Start() => Ice.Gameplay.map.UpdateHeight();

        public static void PlayAt(FXType fx, Vector3 pos, Quaternion? rot = null, float? size = null) => Instance._PlayAt(fx, pos, rot, size);
        public void _PlayAt(FXType fx, Vector3 pos, Quaternion? rot = null, float? size = null)
        {
            var f = particles[fx];
            if (f == null) return;
            f.transform.position = pos;
            f.transform.rotation = rot ?? Quaternion.identity;
            f.transform.localScale = Vector3.one * (size ?? 1);
            f.Input();
        }
    }

    public enum FXType
    {
        PutBuilding,
        DestroyBuilding,
    }
}

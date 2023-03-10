using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class Weapon : MonoBehaviour
    {
        [Group("Weapon")]
        public float interval;
        public float power;
        public LayerMask mask;
        public Transform rotRoot;
        public Transform transRoot;
        public SimpleEvent onFire;

        Hurtable parent;
        float timer = 0;
        void Awake()
        {
            parent = GetComponentInParent<Hurtable>();
        }
        private void Update()
        {
            if (timer > 0) timer -= Time.deltaTime;
        }
        public void TryFire(Hurtable target)
        {
            if (timer > 0) return;

            timer += interval;

            Hurtable hurtedTarget = target;
            Vector3 point = target.AimPos;
            var dir = (target.AimPos - transform.position).normalized;
            Vector3 normal = -dir;
            Debug.DrawRay(transform.position, dir * 100, Color.red, 1);

            if (Physics.Raycast(new Ray(transform.position, dir), out var hit, 1000, mask))
            {
                var tt = hit.collider.GetComponent<Hurtable>();
                if (tt != null)
                {
                    hurtedTarget = tt;
                    point = hit.point;
                    normal = hit.normal;
                }
            }

            if (hurtedTarget != null) hurtedTarget.Hurt(power, parent);

            // 视觉表现
            {
                rotRoot.transform.rotation = Quaternion.LookRotation(dir);
                transRoot.transform.position = point;
                transRoot.transform.rotation = Quaternion.LookRotation(normal);
                onFire?.Invoke();
            }
        }
    }
}

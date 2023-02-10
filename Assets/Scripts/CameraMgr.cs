using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class CameraMgr : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool canMove;
#endif

        [Group("相机")]
        public float edgeSize;
        public float dragSpeed;
        public float moveSpeed;
        public float scrollFactor;
        public Vector2 scrollRange = new Vector2(15, 100);
        [Range(0, 1)] public float focusRate = 0.1f;
        public AnimationCurve rotCurve;

        public Transform focusRoot;
        public Transform focusPoint;
        public Transform focusCamPoint;

        public static CameraMgr Instance { get; private set; }

        public void Focus(Vector3 pos)
        {
            focusRoot.position = pos;
            var p = focusCamPoint.localPosition;
            p.z = -50;
            focusCamPoint.localPosition = p;
        }
        void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            // 平滑缓动
            transform.position += (focusCamPoint.position - transform.position) * (1 - Mathf.Pow(1 - focusRate, Time.deltaTime));
            transform.rotation = Quaternion.Lerp(transform.rotation, focusCamPoint.rotation, 1 - Mathf.Pow(1 - focusRate, Time.deltaTime));

            // 聚焦
            if (Ice.Gameplay.SelectedObject != null && Input.GetKeyDown(KeyCode.Space))
            {
                Focus(Ice.Gameplay.SelectedObject.transform.position);
            }

            // 滚轮缩放
            var localPos = focusCamPoint.localPosition;
            localPos.z = -Mathf.Clamp
                (
                    -localPos.z * (1 - (Input.mouseScrollDelta.y * scrollFactor)),
                    scrollRange.x,
                    scrollRange.y
                );

            focusCamPoint.localPosition = localPos;
            focusPoint.localRotation = Quaternion.Euler(rotCurve.Evaluate(Mathf.InverseLerp(scrollRange.x, scrollRange.y, -localPos.z)), 45, 0);

            // 贴地表
            var pos = focusRoot.position;
            if (Physics.Raycast(new Ray(new Vector3(pos.x, 256, pos.z), Vector3.down), out var hit, 1000, 1 << 6))
            {
                pos.y = hit.point.y;
                focusRoot.position = pos;
            }

            var right = transform.right;
            var fwd = Vector3.Cross(right, Vector3.up);

            if (Input.GetMouseButton(1))
            {
                // 右键移动
                focusRoot.position -= dragSpeed * (-localPos.z) * (right * Input.GetAxis("Mouse X") + fwd * Input.GetAxis("Mouse Y"));
            }
            else
            {
#if UNITY_EDITOR
                if (!canMove) return;
#endif

                // 贴边移动
                var mp = Input.mousePosition;
                float dx = mp.x > Screen.width - edgeSize ? 1 : mp.x < edgeSize ? -1 : 0;
                float dy = mp.y > Screen.height - edgeSize ? 1 : mp.y < edgeSize ? -1 : 0;
                focusRoot.position += moveSpeed * (-localPos.z) * Time.deltaTime * (right * dx + fwd * dy);
            }
        }
    }
}

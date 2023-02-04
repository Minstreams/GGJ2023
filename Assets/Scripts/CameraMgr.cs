using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class CameraMgr : MonoBehaviour
    {
        [Group("相机")]
        public float edgeSize;
        public float dragSpeed;
        public float moveSpeed;
        public float scrollFactor;
        public Vector2 scrollRange = new Vector2(15, 100);
        [Range(0, 1)] public float focusRate = 0.1f;

        public Transform focusPoint;

        public static CameraMgr Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            // 平滑缓动
            transform.position += (focusPoint.position - transform.position) * (1 - Mathf.Pow(1 - focusRate, Time.deltaTime));
            if (Ice.Gameplay.SelectedObject != null && Input.GetKeyDown(KeyCode.Space))
            {
                focusPoint.position = Ice.Gameplay.SelectedObject.transform.position;
            }

            var right = Camera.main.transform.right;
            var fwd = Vector3.Cross(right, Vector3.up);

            if (Input.GetMouseButton(1))
            {
                // 右键移动
                focusPoint.position -= dragSpeed * Camera.main.orthographicSize * (right * Input.GetAxis("Mouse X") + fwd * Input.GetAxis("Mouse Y"));
            }
            else
            {
                // 贴边移动
                var mp = Input.mousePosition;
                float dx = mp.x > Screen.width - edgeSize ? 1 : mp.x < edgeSize ? -1 : 0;
                float dy = mp.y > Screen.height - edgeSize ? 1 : mp.y < edgeSize ? -1 : 0;
                focusPoint.position += moveSpeed * Time.deltaTime * (right * dx + fwd * dy);
            }

            // 滚轮缩放
            Camera.main.orthographicSize =
                Mathf.Clamp
                (
                    Camera.main.orthographicSize * (1 - (Input.mouseScrollDelta.y * scrollFactor)),
                    scrollRange.x,
                    scrollRange.y
                );
        }
    }
}

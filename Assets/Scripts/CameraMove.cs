using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class CameraMove : MonoBehaviour
    {
        public float edgeSize;  //会产生移动效果的边缘宽度
        public float moveAmount;    //移动速度

        public Camera myCamera; //会移动的摄像机

        private Vector3 camFollowPos;   //用于给摄像机赋值

        private bool edgeScrolling = true;  //移动开关
                                            // Start is called before the first frame update
        void Start()
        {
            camFollowPos = myCamera.transform.position; //保存摄像机初始位置，移动是在初始位置的基础上计算
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))    //移动开关
            {
                edgeScrolling = !edgeScrolling;
            }
            if (edgeScrolling)  //如果打开
            {
                //屏幕左下角为坐标(0, 0)
                if (Input.mousePosition.x > Screen.width - edgeSize)//如果鼠标位置在右侧
                {
                    camFollowPos.x += moveAmount * Time.deltaTime;//就向右移动
                }
                if (Input.mousePosition.x < edgeSize)
                {
                    camFollowPos.x -= moveAmount * Time.deltaTime;
                }
                if (Input.mousePosition.y > Screen.height - edgeSize)
                {
                    camFollowPos.z += moveAmount * Time.deltaTime;
                }
                if (Input.mousePosition.y < edgeSize)
                {
                    camFollowPos.z -= moveAmount * Time.deltaTime;
                }
                myCamera.transform.position = camFollowPos;//刷新摄像机位置
            }
        }
    }
}

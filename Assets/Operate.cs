using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operate : MonoBehaviour {
    public Transform box1,box2,box3,box4;
    public float distance = 10.0f;          //  相机距离目标物体默认的距离大小

    public float xSpeed = -250.0f;          //  左右旋转速度
    public float ySpeed = -250.0f;          //  上下旋转速度
    public float yMinLimit = -90.0f;        //  最小旋转上下角度 
    public float yMaxLimit = 90.0f;         //  最大旋转上下角度

    float X = 0.0f;
    float Y = 0.0f;
    float StartDis;

    float XChange;                          //  旋转灵敏度

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        X = angles.y;
        Y = angles.x;
    }

    void Updata()
    {
        //  旋转
        if (Input.touchCount == 1)
        {
            X -= Input.GetTouch(0).deltaPosition.x * xSpeed * Time.deltaTime / 60;  //  +--从左向右旋转，-号--从右向左旋转
            Y += Input.GetTouch(0).deltaPosition.y * ySpeed * Time.deltaTime / 60;  //  +--从上向下旋转

            if (Input.GetTouch(0).deltaPosition.x > 2)
            {
                XChange = 2f;
            }
            else if (Input.GetTouch(0).deltaPosition.x < -2)
            {
                XChange = -2f;
            }
            else
            {
                XChange = Input.GetTouch(0).deltaPosition.x;
            }
        }
        X += XChange;
        if (XChange > 0)
        {
            XChange -= Time.deltaTime * 5;
        }
        else if (XChange < 0)
        {
            XChange += Time.deltaTime * 5;
        }

        //缩放
        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                StartDis = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 tempPosition1 = Input.GetTouch(0).position;
                Vector2 tempPosition2 = Input.GetTouch(1).position;

                float dis = Vector2.Distance(tempPosition1, tempPosition2);
                if (dis < StartDis)
                {
                    if (distance < 130)//远近距离大小  
                    {
                        distance += Time.deltaTime * 7;
                    }
                }
                else if (dis > StartDis)
                {
                    if (distance > 3)
                    {
                        distance -= Time.deltaTime * 7;
                    }
                }
            }
        }
    }
    void LateUpdate()
    {
        if (box1)
        {
            if (Y < -90)//这里控制着上下旋转角度  
            {
                Y = -90;
            }
            if (Y > 180)//这里控制着上下旋转角度  
            {
                Y = 180;
            }
            Y = ClampAngle(Y, yMinLimit, yMaxLimit);
            transform.rotation = Quaternion.Euler(Y, X, 0);
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + box1.position;
        }
        if (box2)
        {
            if (Y < -90)//这里控制着上下旋转角度  
            {
                Y = -90;
            }
            if (Y > 180)//这里控制着上下旋转角度  
            {
                Y = 180;
            }
            Y = ClampAngle(Y, yMinLimit, yMaxLimit);
            transform.rotation = Quaternion.Euler(Y, X, 0);
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + box2.position;
        }
        if (box3)
        {
            if (Y < -90)//这里控制着上下旋转角度  
            {
                Y = -90;
            }
            if (Y > 180)//这里控制着上下旋转角度  
            {
                Y = 180;
            }
            Y = ClampAngle(Y, yMinLimit, yMaxLimit);
            transform.rotation = Quaternion.Euler(Y, X, 0);
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + box3.position;
        }
        if (box4)
        {
            if (Y < -90)//这里控制着上下旋转角度  
            {
                Y = -90;
            }
            if (Y > 180)//这里控制着上下旋转角度  
            {
                Y = 180;
            }
            Y = ClampAngle(Y, yMinLimit, yMaxLimit);
            transform.rotation = Quaternion.Euler(Y, X, 0);
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + box4.position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
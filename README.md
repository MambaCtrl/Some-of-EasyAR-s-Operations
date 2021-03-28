# Some-of-EasyAR-s-Operations
## EasyAR的HellAR中改写实现将视频存储在学校网站，避免发布的apk过大，并实现用户手指操作模型缩放旋转等  
>**要求：**  
>**1.untiy版本：2017.1.1**  
>**2.EasyAR SDK 2.0**  
>**3.[下载winscp](https://winscp.net/eng/docs/lang:chs)，通过winscp将视频位置放在学校vr招生网站上**

## readme  
>**具体步骤：** 
>###**一.线上播放视频**   
>**1.登录easyconnect和winscp，将视频上传到线上**  
>**2.将ImageTarget预制体拖入到Hierarchy面板中去，并重命名为"ImageTarget-URL_Video"**
>**3.新建SampleImageTargetBehaviour.cs脚本并附在ImageTarget-URL_Video上**  
>```SampleImageTargetBehaviour.cs 
>using UnityEngine;    
>using EasyAR;    
>public class SampleImageTargetBehaviour :ImageTargetBehaviour {  
>      protected override void Awake()   
>      {  
>          base.Awake();    
>          TargetFound += OnTargetFound;  
>          TargetLost += OnTargetLost;  
>          TargetLoad += OnTargetLoad;  
>          TargetUnload += OnTargetUnload;  
>      }  
>
>      void OnTargetFound(TargetAbstractBehaviour behaviour)
>      {
>           Debug.Log("Found: " + Target.Id);
>      }
> 
>      void OnTargetLost(TargetAbstractBehaviour behaviour)
>      {
>           Debug.Log("Lost: " + Target.Id);
>      }
> 
>      void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
>      {
>           Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
>      }
> 
>      void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
>      {
>          Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
>      }
>}
>```  
>**4.ImageTarget-URL_Video右键新建plane用于播放视频,再把EasyAR->Scripts->VideoPlayerBehaviour.cs这个脚本挂载到Plane**    
>**5.修改第3步代码如下**  
>```using UnityEngine;  
>using EasyAR;  
>public class SampleImageTargetBehaviour : ImageTargetBehaviour  
>{  
>    private string video = @"https://lq.hdu.edu.cn/czw/video.mp4";                                 // 我的视频地址
>    protected override void Awake()  
>    {  
>        base.Awake();  
>        TargetFound += OnTargetFound;  
>       TargetLost += OnTargetLost;  
>        TargetLoad += OnTargetLoad;  
>        TargetUnload += OnTargetUnload;  
>    }  
>
>    void OnTargetFound(TargetAbstractBehaviour behaviour)
>    {
>        Debug.Log("Found: " + Target.Id);
>    }
>
>    void OnTargetLost(TargetAbstractBehaviour behaviour)
>    {
>        Debug.Log("Lost: " + Target.Id);
>    }
>
>    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
>    {
>        Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
>    }
>
>    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
>    {
>        Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
>    }
>
>    public void LoadVideo()
>    {
>        GameObject subGameObject = Instantiate(Resources.Load("Plane", typeof(GameObject))) as GameObject;
>        subGameObject.transform.parent = this.transform;
>        subGameObject.transform.localPosition = new Vector3(0, 0.225f, 0);                       // 位置，数值可以自己设置
>        subGameObject.transform.localRotation = new Quaternion();                                // 旋转，数值可以自己设置
>        subGameObject.transform.localScale = new Vector3(0.8f, 0.45f, 0.45f);                    // 缩放，数值可以自己设置
>
>        VideoPlayerBaseBehaviour videoPlayer = subGameObject.GetComponent<VideoPlayerBaseBehaviour>();
>        if (videoPlayer)
>        {
>            videoPlayer.Storage = StorageType.Absolute;
>            videoPlayer.Path = video;
>            videoPlayer.EnableAutoPlay = true;                                                   // 自动播放
>            videoPlayer.EnableLoop = true;                                                       // 循环播放
>            videoPlayer.Open();
>        }
>    }
>    protected override void Start()
>    {
>        base.Start();
>        LoadVideo();
>    }
>}
>```
>### **二.手控制模型动画缩放旋转等**  
>新建MyOperation.cs附在object上（一定要附在物体上不要附在camera上）  
>```using UnityEngine;
>using System.Collections;
>
>public class MyOperation : MonoBehaviour {
>
>    private Touch oldTouch1;  //上次触摸点1(手指1)  
>    private Touch oldTouch2;  //上次触摸点2(手指2)  
>    public Transform box;
>
>    public float distance = 15.0f;      //相机距离目标物体默认距离大小  
>
>    public float xSpeed = -250.0f;      //X（左右旋转速度）  
>    public float ySpeed = -250.0f;      //（上下旋转速度）  
>    public float yMinLimit = -90;       //旋转上下角度最小限制  
>    public float yMaxLimit = 90;
>
>    float X = 0.0f;
>    float Y = 0.0f;
>    float StartDis;
>
>    float XChange;                      //旋转灵敏度
>    void Start()
>    {
>        Vector3 angles = transform.eulerAngles;
>        X = angles.y;
>        Y = angles.x;
>    }
>
>    private void OnMouseDown()
>    {
>        print("czw handsome");
>    }
>
>
>    void Update()
>    {
>
>        //没有触摸  
>        if (Input.touchCount <= 0)
>        {
>            return;
>        }
>        //旋转
>        if (Input.touchCount == 1)
>        {
>            X -= Input.GetTouch(0).deltaPosition.y * ySpeed * Time.deltaTime / 60;   //+和-决定从左向右旋转还是从右向左转  
>            Y += Input.GetTouch(0).deltaPosition.x * xSpeed * Time.deltaTime / 60;  //+和-决定从上向下旋转还是从下而上转  
>            if (Input.GetTouch(0).phase == TouchPhase.Ended)
>            {
>                if (Input.GetTouch(0).deltaPosition.x > 2)
>                {
>                    XChange = 2f;
>                }
>                else if (Input.GetTouch(0).deltaPosition.x < -2)
>                {
>                    XChange = -2f;
>                }
>                else
>                {
>                    XChange = Input.GetTouch(0).deltaPosition.x;
>                }
>            }
>        }
>
>        X += XChange;
>        if (XChange > 0)
>        {
>            XChange -= Time.deltaTime * 5;
>        }
>        else if (XChange < 0)
>        {
>            XChange += Time.deltaTime * 5;
>        }
>        //单点触摸， 水平上下旋转  
>        //if (1 == Input.touchCount)
>        //{
>        //    print("wocaoaidjaijda");
>        //    Touch touch = Input.GetTouch(0);
>        //    Vector2 deltaPos = touch.deltaPosition;
>        //    transform.localEulerAngles += new Vector3(0, 0, deltaPos.x);
>        //    transform.localEulerAngles += new Vector3(0, deltaPos.y, 0);
>        //    //transform.Rotate(Vector3.down * deltaPos.x, Space.World);
>        //    //transform.Rotate(Vector3.right * deltaPos.y, Space.World);
>        //}
>
>        //多点触摸, 放大缩小  
>        Touch newTouch1 = Input.GetTouch(0);
>        Touch newTouch2 = Input.GetTouch(1);
>
>        //第2点刚开始接触屏幕, 只记录，不做处理  
>        if (newTouch2.phase == TouchPhase.Began)
>        {
>            oldTouch2 = newTouch2;
>            oldTouch1 = newTouch1;
>            return;
>        }
>
>        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
>        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
>        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
>
>        //两个距离之差，为正表示放大手势， 为负表示缩小手势  
>        float offset = newDistance - oldDistance;
>
>        //放大因子， 一个像素按 0.01倍来算(100可调整)  
>        float scaleFactor = offset / 100f;
>        Vector3 localScale = transform.localScale;
>        Vector3 scale = new Vector3(localScale.x + scaleFactor,
>        localScale.y + scaleFactor,
>        localScale.z + scaleFactor);
>
>        //最小缩放到 0.3 倍  
>        if (scale.x > 0.3f && scale.y > 0.3f && scale.z > 0.3f)
>        {
>            transform.localScale = scale;
>        }
>
>        //记住最新的触摸点，下次使用  
>        oldTouch1 = newTouch1;
>        oldTouch2 = newTouch2;
>    }
>    void LateUpdate()
>    {
>        if (box)
>        {
>            if (Y < -90)    //控制着上下旋转角度  
>            {
>                Y = -90;
>            }
>            if (Y > 180)    //控制着上下旋转角度  
>            {
>                Y = 180;
>            }
>            Y = ClampAngle(Y, yMinLimit, yMaxLimit);
>            transform.rotation = Quaternion.Euler(0, X, Y);
>           // transform.position = transform.rotation * new Vector3(0, 0, -distance) + box.position;
>        }
>    }
>
>    static float ClampAngle(float angle, float min, float max)
>    {
>        if (angle < -360)
>            angle += 360;
>        if (angle > 360)
>            angle -= 360;
>        return Mathf.Clamp(angle, min, max);
>    }
>}
>```

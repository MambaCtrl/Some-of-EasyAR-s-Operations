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
>###**二.手控制模型动画缩放旋转等**  
>

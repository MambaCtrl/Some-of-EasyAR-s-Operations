# Some-of-EasyAR-s-Operations
## EasyAR的HellAR中改写实现将视频存储在学校网站，避免发布的apk过大，然后实现用户手指操作模型缩放旋转等  
>**要求：**  
>**1.untiy版本：2017.1.1**  
>**2.EasyAR SDK 2.0**  
>**3.[下载winscp](https://winscp.net/eng/docs/lang:chs)，通过winscp将视频位置放在学校vr招生网站上**

## readme  
>**具体步骤：**  
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
```  
>**4.ImageTarget-URL_Video右键新建plane用于播放视频**  
>
>
>

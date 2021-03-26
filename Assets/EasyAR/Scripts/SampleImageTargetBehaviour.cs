using UnityEngine;
using EasyAR;

public class SampleImageTargetBehaviour : ImageTargetBehaviour
{
    private string video = @"https://lq.hdu.edu.cn/czw/video.mp4";
    protected override void Awake()
    {
        base.Awake();
        TargetFound += OnTargetFound;
        TargetLost += OnTargetLost;
        TargetLoad += OnTargetLoad;
        TargetUnload += OnTargetUnload;
    }

    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
        Debug.Log("Found: " + Target.Id);
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
        Debug.Log("Lost: " + Target.Id);
    }

    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
        Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }

    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
        Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }

    public void LoadVideo()
    {
        GameObject subGameObject = Instantiate(Resources.Load("Plane", typeof(GameObject))) as GameObject;
        subGameObject.transform.parent = this.transform;
        subGameObject.transform.localPosition = new Vector3(0, 0.225f, 0);//位置，数值可以自己设置
        subGameObject.transform.localRotation = new Quaternion();//旋转，数值可以自己设置
        subGameObject.transform.localScale = new Vector3(0.8f, 0.45f, 0.45f);//缩放，数值可以自己设置

        VideoPlayerBaseBehaviour videoPlayer = subGameObject.GetComponent<VideoPlayerBaseBehaviour>();
        if (videoPlayer)
        {
            videoPlayer.Storage = StorageType.Absolute;
            videoPlayer.Path = video;
            videoPlayer.EnableAutoPlay = true;//自动播放
            videoPlayer.EnableLoop = true;//循环播放
            videoPlayer.Open();
        }
    }
    protected override void Start()
    {
        base.Start();
        LoadVideo();
    }
}

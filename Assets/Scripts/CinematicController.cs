using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {   
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {   
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] GameObject player;


    void Start()
    {   
        player.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {   
        player.SetActive(true);
        gameObject.SetActive(false);
    }
}

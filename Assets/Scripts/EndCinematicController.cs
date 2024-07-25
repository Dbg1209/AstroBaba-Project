using UnityEngine;
using UnityEngine.Video;

public class EndCinematicController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] GameObject player;

    void Start()
    {   
        player.SetActive(false);
        GameManager.Instance.PauseGame();
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        GameManager.Instance.Victory();
    }
}

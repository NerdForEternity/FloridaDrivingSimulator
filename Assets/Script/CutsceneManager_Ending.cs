using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager_Ending : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndGame;
            videoPlayer.Play();
        }
    }

    void EndGame(VideoPlayer vp)
    {
        vp.Stop();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

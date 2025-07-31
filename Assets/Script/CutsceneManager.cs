using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject cutsceneCanvas; // optional: hide overlay/UI

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndCutscene;
            videoPlayer.Play();
        }
    }

    private void EndCutscene(VideoPlayer vp)
    {
        vp.Stop();
        if (cutsceneCanvas != null)
            cutsceneCanvas.SetActive(false);

        // Load the HubScene after the cutscene finishes
        UnityEngine.SceneManagement.SceneManager.LoadScene("HubScene");

        this.enabled = false;
    }
}
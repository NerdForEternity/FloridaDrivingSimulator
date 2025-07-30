using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Elements")]
    public Image cutsceneImage;
    public AudioSource voiceoverAudio;
    public float cutsceneDuration = 5f; // Default duration (can change per scene)

    [Header("Scene Settings")]
    public bool isOpeningCutscene = false;
    public string nextSceneName; // Set this for opening cutscene to load the hub or level 1

    void Start()
    {
        if (isOpeningCutscene)
            PlayCutscene();
    }

    public void PlayCutscene()
    {
        cutsceneImage.gameObject.SetActive(true);
        if (voiceoverAudio != null)
            voiceoverAudio.Play();

        Invoke(nameof(EndCutscene), cutsceneDuration);
    }

    public void EndCutscene()
    {
        cutsceneImage.gameObject.SetActive(false);
        if (isOpeningCutscene)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // End cutscene for victory—return to menu or hub
            SceneManager.LoadScene("HubLevel"); // Change this as needed
        }
    }
}

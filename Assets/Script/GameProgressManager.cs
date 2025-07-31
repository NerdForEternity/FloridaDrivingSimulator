using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    private bool level1Complete = false;
    private bool level2Complete = false;
    private bool level3Complete = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteLevel(string levelName)
    {
        Debug.Log($"CompleteLevel called: {levelName}");

        if (levelName == "Level1Scene")
            level1Complete = true;
        else if (levelName == "Level2")
            level2Complete = true;
        else if (levelName == "Level3")
            level3Complete = true;
        else
            Debug.LogWarning("Unknown level name in CompleteLevel");

        if (level1Complete && level2Complete && level3Complete)
        {
            Debug.Log("All levels complete! Loading EndingCutsceneScene...");
            SceneManager.LoadScene("EndingCutsceneScene");
        }
    }
}

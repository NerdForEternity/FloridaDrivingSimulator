using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.name);
        // then the same if checks
    }
    public void LoadLevel1()
    {
        
        SceneManager.LoadScene("Level1Scene");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadHub()
    {
        SceneManager.LoadScene("HubScene");
    }
}

using UnityEngine;

public class CarHitbox : MonoBehaviour
{
    public AudioClip destroySound;  // Assign in inspector
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.gameObject.name); // Log hit

        if (other.CompareTag("Hittable"))
        {
            ScoreManager.Instance.AddScore(50);
            ScoreManager.Instance.AddStyle(10);
            // Play destroy sound
            if (destroySound != null)
                audioSource.PlayOneShot(destroySound);
            Destroy(other.gameObject);
            Debug.Log("Destroyed: " + other.name);
        }
    }
}
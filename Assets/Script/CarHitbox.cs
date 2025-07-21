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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hittable"))
        {
            ScoreManager.Instance.AddScore(100);
            ScoreManager.Instance.AddStyle(10);

            // Play destroy sound
            if (destroySound != null)
                audioSource.PlayOneShot(destroySound);

            // Destroy the object after playing the sound
            Destroy(collision.gameObject);
        }
    }
}
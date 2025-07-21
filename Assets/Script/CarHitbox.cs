using UnityEngine;

public class CarHitbox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name); 

        if (collision.gameObject.CompareTag("Hittable"))
        {
            ScoreManager.Instance.AddScore(100);
            ScoreManager.Instance.AddStyle(10);
            Destroy(collision.gameObject); // This removes the objects with the tag Hittable
        }
    }
}
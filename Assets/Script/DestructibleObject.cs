using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject explosionSprite;
    public float spriteLifetime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
        SpawnAndDestroyExplosion();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 cubePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAndDestroyExplosion()
    {
        GameObject explosionClone = Instantiate(explosionSprite);
        Destroy(explosionClone, spriteLifetime);
        Debug.Log("Destroyed Sprite");
    }

}

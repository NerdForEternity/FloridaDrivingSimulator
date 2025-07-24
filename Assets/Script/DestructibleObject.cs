using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject explosionSprite;
    public float spriteLifetime = 3f;
    //Transform objectTransform = GetComponent<Transform>().position;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
             
        SpawnAndDestroyExplosion();
    }

    //Method to spawn and destroy the explosion after destroying the cube
    void SpawnAndDestroyExplosion()
    {
        Vector3 cubePosition = transform.position; // Get the position of the gameobject this script is attached to
        Destroy(gameObject); // Destroy the GameObject this script is attached to
        GameObject explosionClone = Instantiate(explosionSprite, cubePosition, Quaternion.identity);
        Destroy(explosionClone, spriteLifetime);
        Debug.Log("Destroyed Sprite");
    }

}

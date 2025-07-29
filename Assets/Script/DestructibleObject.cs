using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public List<GameObject> explosionList;
    public float spriteLifetime = 3f;
     

    
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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Detected Player");
            SpawnAndDestroyExplosion();
        }   
        
    }
   
    //Method to spawn and destroy the explosion after destroying the cube
    void SpawnAndDestroyExplosion()
    {
        Vector3 cubePosition = transform.position; // Get the position of the gameobject this script is attached to
        int listMax = explosionList.Count; //Get the maximum length of the explosionList
        Destroy(gameObject); // Destroy the GameObject this script is attached to
        GameObject explosionClone = Instantiate(explosionList[Random.Range(0, listMax)], cubePosition, Quaternion.identity);
        Destroy(explosionClone, spriteLifetime);
        Debug.Log("Destroyed Sprite");
    }

}

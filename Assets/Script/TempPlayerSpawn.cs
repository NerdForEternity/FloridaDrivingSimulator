using UnityEngine;

public class TempPlayerSpawn : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(player, new Vector3(-20,0,-20), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

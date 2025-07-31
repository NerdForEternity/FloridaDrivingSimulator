using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    int collectCount;

    private void OnEnable()
    {
        ActionListener.OnTruckCollected += TruckCollected;
    }
    
    private void OnDisable()
    {
        ActionListener.OnTruckCollected -= TruckCollected;
    }

    private void Awake()
    {
        collectCount = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TruckCollected()
    {
        collectCount++;
        Debug.Log("Player has found " + collectCount + " collectibles!");
    }

}

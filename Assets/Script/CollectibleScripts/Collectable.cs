using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public abstract void OnCollect();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollect(); //Trigger event
            Destroy(gameObject);
        } 
    }

}

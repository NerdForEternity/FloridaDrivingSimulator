using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    // public int damageDealt = 1;
    public PlayerControllerHB playerControllerHB;

    private void OnCollisionEnter(Collision collision)
    {
      CarController carController = GetComponent<CarController>();
        if (carController == null )
        {
            playerControllerHB.TakeDamage(-1);
        }
        
        //  if (collision.gameObject.tag == "Player")
           // {
           // playerControllerHB.TakeDamage(damageDealt);
           // }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    // public float damageZone;
    //public int damageDealt = -1;
    public PlayerControllerHB playerControllerHB;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            {
            playerControllerHB.TakeDamage(1);
            }
    }


}

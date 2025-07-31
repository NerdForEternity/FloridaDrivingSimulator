using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public int damage = 2;
    private PlayerControllerHB playerControllerHB;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (playerControllerHB == null)
            {
                playerControllerHB = collision.gameObject.GetComponent<PlayerControllerHB>();
            }
            playerControllerHB.TakeDamage(damage);
        }
    }

}

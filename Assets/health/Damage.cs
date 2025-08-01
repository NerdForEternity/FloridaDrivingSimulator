using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private PlayerControllerHB playerControllerHB;

    public int damageAmount = 1; 

    private void Start()
    {
        playerControllerHB = GetComponent<PlayerControllerHB>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            playerControllerHB.TakeDamage(damageAmount);
        }
    }

}

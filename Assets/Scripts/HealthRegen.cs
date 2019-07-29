using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    void PickUp(Collider2D player)
    {
        //Instantiate(PickUpEffect, transform.position, transform.rotation);
        PlayerStatus status = player.GetComponent<PlayerStatus>();
        status.health += 100f;
        Destroy(gameObject);
 
    }

}

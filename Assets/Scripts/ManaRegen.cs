using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegen : MonoBehaviour
{
    public GameObject PickUpEffect;
    public float multiplicator = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine( PickUp(other) );
            
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        //Instantiate(PickUpEffect, transform.position, transform.rotation);
        PlayerStatus status = player.GetComponent<PlayerStatus>();

        status.manaRegen *= multiplicator;
        status.fireCooldownTime *= 0.5f;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(5f);
        status.manaRegen /= multiplicator;
        status.fireCooldownTime /= 0.5f;

        Destroy(gameObject);



    }

}

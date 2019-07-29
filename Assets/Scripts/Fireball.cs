using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Level"))
        {
            //animation
            Destroy(gameObject,.2f);
        }
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(30f);
            Debug.Log("Enemy hit");
            Destroy(gameObject, .2f);
        }
    }
}

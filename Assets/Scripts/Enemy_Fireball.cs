using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fireball : MonoBehaviour
{
    public Animator animationController;
    public void Start()
    {
        animationController = GetComponent<Animator>();
        animationController.SetBool("Exploded", false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Level"))
        {
            //animation
            animationController.SetBool("Exploded", true);
            Destroy(gameObject, .2f);
        }
        if (other.CompareTag("Player"))
        {
            //animation
            animationController.SetBool("Exploded", true);
            other.GetComponent<Player>().TakeDamage(30f);
            Destroy(gameObject, .2f);
        }

    }
}

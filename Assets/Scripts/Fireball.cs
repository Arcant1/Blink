using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Animator animationController;

    public void Start()
    {
        animationController = GetComponent<Animator>();
        animationController.SetBool("Exploded", false);
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Level"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<AudioSource>().Play();

            animationController.SetBool("Exploded", true);
            Debug.Log("hit level");
            Destroy(gameObject,.8f);
        }
        if(other.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            GetComponent<AudioSource>().Play();

            animationController.SetBool("Exploded", true);

            other.GetComponent<Enemy>().TakeDamage(30f);
            Destroy(gameObject, .8f);
        }
        
    }
}

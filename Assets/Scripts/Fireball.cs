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
            animationController.SetBool("Exploded", true);
            Debug.Log("hit level");
            Destroy(gameObject,.2f);
        }
        if(other.CompareTag("Enemy"))
        {
            animationController.SetBool("Exploded", true);

            other.GetComponent<Enemy>().TakeDamage(30f);
            Destroy(gameObject, .2f);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();

            PickUp(other);
        }
        if (other.CompareTag("Enemy"))
        {
            GetComponent<AudioSource>().Play();
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.health < 100f)
            {
                enemy.health = 100f;
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, .8f);
                enemy.ResumeSearch();
            }
        }
    }

    void PickUp(Collider2D player)
    {
        PlayerStatus status = player.GetComponent<PlayerStatus>();
        Player _player = player.GetComponent<Player>();

        status.health = 100f;
        _player.healthBar.fillAmount = status.health / 100f;
        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject, .8f);

    }

}

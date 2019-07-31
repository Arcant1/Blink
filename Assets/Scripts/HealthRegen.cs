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
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.health < 100f)
            {
                enemy.health = 100f;
                gameObject.SetActive(false);
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
        gameObject.SetActive(false);

    }

}

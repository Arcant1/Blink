using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float mana = 100;
    public Image healthBar;
    public Image manaBar;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
            Die();
        healthBar.fillAmount = health / 100f;

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

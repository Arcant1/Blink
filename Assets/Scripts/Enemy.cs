using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float mana = 100;
    public float manaRegen = .25f;
    public Stack<Transform> items;
    public float horizontal;
    public float vertical;
    public float magnitude;
    private float dashCooldownTime = 1f;
    private float fireCooldownTime = .33f;
    private float nextDashTime = 0;
    private float nextFireTime = 0;
    public Animator animatorController;
    public GameObject spell;
    public EnemyIA iA;
    public Image healthBar;
    public Image manaBar;
    public int totalItems = 0;
    public List<int> idList;
    public AudioSource shootSound;
    public AudioSource dashSound;

    public void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        shootSound = audios[1];
        dashSound = audios[0];
        idList = new List<int>();
        items = new Stack<Transform>();
    }




    public void ResumeSearch()
    {
        iA.target = iA.player.transform;
        iA.maxDistanceToPlayer = 4f;
        healthBar.fillAmount = health / 100f;
        iA.lookingForHealth = false;

    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / 100f;
        if (health < 0)
            Die();
        healthBar.fillAmount = health / 100f;

    }

    public void Die()
    {
        //animation
        Destroy(gameObject);
    }

    public void Dash()
    {
        if (nextDashTime < Time.time)
            if (mana > 50)
            {
                dashSound.Play();
                mana -= 50f;
                manaBar.fillAmount = mana / 100f;
                nextDashTime = Time.time + dashCooldownTime;
                transform.position = transform.position + Vector3.right * 1f;//dash towards a direction
            }

    }

    public void Update()
    {
        if (mana < 100f)
        {
            mana += manaRegen;
            manaBar.fillAmount = mana / 100f;
        }
        totalItems = items.Count;
        magnitude = horizontal * horizontal + vertical * vertical;
        animatorController.SetFloat("Horizontal", horizontal);
        animatorController.SetFloat("Vertical", vertical);
        animatorController.SetFloat("Magnitude", magnitude);
    }

    public void Shoot()
    {
        if (nextFireTime < Time.time)
            if (mana > 80)
            {
                shootSound.Play();
                mana -= 30;
                manaBar.fillAmount = mana / 100f;
                GameObject fireball = Instantiate(spell, transform.position, Quaternion.identity);
                fireball.transform.Rotate(0f, 0f, Mathf.Atan2(iA.target.transform.position.y - transform.position.y, iA.target.transform.position.x - transform.position.x) * Mathf.Rad2Deg);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(iA.target.transform.position.x - transform.position.x, iA.target.transform.position.y - transform.position.y).normalized * 5f;
                nextFireTime = Time.time + fireCooldownTime;
                Destroy(fireball, 2f);
            }      
    }

    
    

}

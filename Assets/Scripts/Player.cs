using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    const float MAX_RANGE = 3f;

    public bool WASD;




    public PlayerStatus status;
    public AudioSource dashSound;
    public AudioSource shootSound;

    [Header("Unity Struff")]
    public Image healthBar;
    public Image manaBar;
    [HideInInspector]
    public float horizontal;
    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public float magnitude;
    public Rigidbody2D rb;
    public Animator animatorController;
    public GameObject spell;
    public GameObject crossair;
    [HideInInspector]
    public Vector3 hitDestination;
    [HideInInspector]
    public Vector3 VectorToMouse;



    Vector3 dashDirection;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios =GetComponents<AudioSource>();
        shootSound = audios[0];
        dashSound = audios[1];
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.mass = 1f;
        rb.drag = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        if (status.nextDashTime < Time.time)
            if (Input.GetMouseButtonDown(1))
            {
                Dash();
                status.nextDashTime = Time.time + status.dashCooldownTime;
            }

        if (status.nextFireTime < Time.time)
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                status.nextFireTime = Time.time + status.fireCooldownTime;
            }


    }

    void GetInputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animatorController.SetFloat("Horizontal", horizontal);
        animatorController.SetFloat("Vertical", vertical);
        dashDirection = new Vector3(horizontal, vertical, 0f);
        magnitude = dashDirection.magnitude;
        animatorController.SetFloat("Magnitude", magnitude);
        dashDirection.Normalize();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hitDestination = new Vector3(ray.origin.x, ray.origin.y, 0f);

        VectorToMouse = (hitDestination - transform.position).normalized;
        crossair.transform.localPosition = VectorToMouse;

    }

    void Dash()
    {
        if (status.mana > 50)
        {
            dashSound.Play();
            status.mana -= 50f;
            if (!WASD)
            {
                if ((transform.position - hitDestination).magnitude < MAX_RANGE)
                {
                    transform.position = hitDestination;
                }
                else
                {
                    transform.position += (hitDestination - transform.position).normalized * MAX_RANGE;
                }
            }
            else
                transform.position += dashDirection * MAX_RANGE;
        }

    }

    void Shoot()
    {
        if (status.mana > 30)
        {
            shootSound.Play();
            animatorController.SetTrigger("Attack");
            status.mana -= 30;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hitDestination = new Vector3(ray.origin.x, ray.origin.y, 0f);
            GameObject fireball = Instantiate(spell, transform.position, Quaternion.identity);
            fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(VectorToMouse.x, VectorToMouse.y).normalized * 5f+rb.velocity;
            fireball.transform.Rotate(0f, 0f, Mathf.Atan2(VectorToMouse.y, VectorToMouse.x) * Mathf.Rad2Deg);
            Destroy(fireball, 2f);
        }
    }

    void FixedUpdate()
    {
        manaBar.fillAmount = status.mana / status.maxMana;
        transform.Translate(dashDirection * status.speed * Time.fixedDeltaTime);
        if (status.mana < 100)
            status.mana += status.manaRegen;
    }

    public void TakeDamage(float amount)
    {
        status.health -= amount;
        healthBar.fillAmount = status.health / status.maxHealth;
        if (status.health < 0)
        {
            Die();
        }
        manaBar.fillAmount = status.mana / status.maxMana;

    }

    public void Die()
    {
        SceneManager.LoadScene(2);

    }

}

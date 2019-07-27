using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float MAX_SPEED = 4f;
    const float MAX_RANGE = 1f;
    public float horizontal;
    public float vertical;
    public float magnitude;
    public Rigidbody2D rb;
    public Animator animatorController;
    public float velocity;
    public GameObject spell;
    Vector3 destination;

    Vector2 aux;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.mass = 1f;
        rb.drag = 10f;
        velocity = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animatorController.SetFloat("Horizontal", horizontal);
        animatorController.SetFloat("Vertical", vertical);
        aux = new Vector2(horizontal, vertical);
        magnitude = aux.magnitude;
        animatorController.SetFloat("Magnitude", magnitude);
        aux.Normalize();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            destination = new Vector3(ray.origin.x, ray.origin.y, 0f);

            if ((transform.position - destination).magnitude < MAX_RANGE)
            {
                transform.position = destination;
            }
            else
            {
                transform.position += (destination - transform.position).normalized * MAX_RANGE;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            destination = new Vector3(ray.origin.x, ray.origin.y, 0f);
            GameObject ball = Instantiate(spell, transform.position,Quaternion.Euler(destination));
            
            Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.velocity = 2 * (destination - transform.position).normalized;
            ball.transform.rotation = Quaternion.Euler((destination - transform.position).normalized);
        }


        }

    

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < MAX_SPEED)
            rb.velocity += aux * velocity * Time.fixedDeltaTime;
        else
            rb.velocity = aux * MAX_SPEED;
    }
}

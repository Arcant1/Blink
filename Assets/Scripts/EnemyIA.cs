using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyIA : MonoBehaviour
{
    public Transform target;
    public Transform enemyPosition;
    public Enemy enemy;
    public Player player;
    public float updateDelay = 2f;

    public bool lookingForHealth = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;
    public float distanceToPlayer;
    public float maxDistanceToPlayer = 4f;
    public float distanceToShoot = 3f;
    public Vector3 direction;
    public float speed = 300f;

    public ForceMode2D forceMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    public float nextWaypointDistance = 3f;

    public int currentWaypoint = 0;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.Log("No player found");
            return;
        }

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        enemy.horizontal = (direction.x > 0) ? 1 : (direction.x < 0) ? -1 : 0;
        enemy.vertical = (direction.y > 0) ? 1 : (direction.y < 0) ? -1 : 0;
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            //TODO: insert a target here
            yield return null;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateDelay);
        StartCoroutine(UpdatePath());

    }

    void OnPathComplete(Path _path)
    {
        //Debug.Log("The path has an error?" + _path.error);
        if (!_path.error)
        {
            path = _path;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {

        //check for errors
        if (target == null)
            return;
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //find direction to next waypoint
        direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        direction *= speed * Time.fixedDeltaTime;

        enemyPosition = player.transform;

        float distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //move the enemy towards target
        rb.AddForce(direction, forceMode);

        if (distanceToPlayer < distanceToShoot)
        {
            if (!lookingForHealth)
                enemy.Shoot();
        }

        if ((enemy.health < 50f) && (!lookingForHealth))
        {
            if (enemy.items.Count > 0)
            {
                Transform aux = enemy.items.Pop();
                target = (aux != null) ? aux : player.transform;
                maxDistanceToPlayer = 0;
                lookingForHealth = true;
            }
        }

        if (distanceToWaypoint < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public Enemy enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("PowerUp") && (!enemy.idList.Contains(other.GetInstanceID()))))
        {
            enemy.idList.Add(other.GetInstanceID());
            enemy.items.Push(other.transform);
        }

        if (other.CompareTag("Projectile"))
        {
            enemy.Dash();
            Debug.Log("DASH");
        }
    }
}

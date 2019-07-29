using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health = 100f;
    public float mana = 100f;
    public float manaRegen = 0.25f;
    public float speed = 3f;
    [HideInInspector]
    const float MAX_SPEED = 4f;
    [HideInInspector]
    public float maxHealth = 100f;
    [HideInInspector]
    public float maxMana = 100f;

   

}

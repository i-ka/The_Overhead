using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour {

    public int damage;
    public int health=100;
    public float attackCoolDown = 1.0f;
    [HideInInspector]
    public bool isAlive = true;

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            isAlive = false;
        }
    }
}

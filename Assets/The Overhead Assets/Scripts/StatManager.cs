using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMoveController))]
public class StatManager : MonoBehaviour {

    public int damage;
    public int health=100;
    public float attackCoolDown = 1.0f;
    public float pushBackForce = 100;
    public float maxSpeed = 30;
    public float jumpPower = 30;
    [HideInInspector]
    public bool isAlive = true;

    void Start()
    {
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            isAlive = false;
        }
    }
}

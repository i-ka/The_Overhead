using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {

    public int damage;
    public int health=100;
    public float attackCoolDown = 1.0f;
    public float pushBackForce = 100;
    public Slider healthSlider;
    [HideInInspector]
    public bool isAlive = true;

    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0) {
            isAlive = false;
        }
    }
}

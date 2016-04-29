using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour {

    public int damage;
    public int health=100;

    public void ApplyDamage(int damage)
    {
        health -= damage;
        print("Take " + damage.ToString() + " damage!");
    }
}

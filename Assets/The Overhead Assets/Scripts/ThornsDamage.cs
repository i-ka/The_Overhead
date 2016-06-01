using UnityEngine;
using System.Collections;

public class ThornsDamage : MonoBehaviour
{
    public int damage;
    public int pushBackForce;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessageUpwards("ApplyDamage", damage);
            Vector2 pushVector = new Vector2(other.transform.position.x - transform.position.x, 20);
            other.GetComponent<CharacterMoveController>().pushBack(pushVector.normalized * pushBackForce);
        }
    }
}

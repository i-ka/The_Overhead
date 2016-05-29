using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    private CharacterMoveController m_platformerMove;

    void Start()
    {
        m_platformerMove = GetComponentInParent<CharacterMoveController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(m_platformerMove.m_enemyTag))
        {
            other.SendMessageUpwards("ApplyDamage", m_platformerMove.stats.damage);
            Vector2 pushVector = new Vector2(other.transform.position.x - transform.position.x, 2);
            other.GetComponent<CharacterMoveController>().pushBack(pushVector.normalized * m_platformerMove.stats.pushBackForce);
        }
    }
}

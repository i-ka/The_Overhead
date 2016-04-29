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
        if (other.isTrigger && other.CompareTag(m_platformerMove.m_enemyTag))
        {
            print("some");
            other.SendMessageUpwards("ApplyDamage", m_platformerMove.m_stats.damage);
        }
    }
}

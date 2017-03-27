using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTrigger : MonoBehaviour {

    private CharacterMoveController m_character;
    public int Damage { get; set; }
    public float PushBackForce { get; set; }
    private List<string> _affectedTags = new List<string>();
    public List<string> AffectedTags { get { return _affectedTags; }}

    void Start()
    {
        m_character = GetComponentInParent<CharacterMoveController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Utils.CheckTags(other.gameObject, AffectedTags)/*other.CompareTag(m_character.EnemyTag)*/)
        {
            var attackMessage = new AttackMessage(Damage,
                new Vector2(other.transform.position.x - transform.position.x, 2).normalized * PushBackForce);
            other.SendMessageUpwards("ApplyDamage", attackMessage);
        }
    }
}

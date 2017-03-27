using UnityEngine;
using System.Collections;

public class ThornsDamage : MonoBehaviour
{
    public int damage;
    public int pushBackForce;
    // Use this for initialization
    void Start()
    {
        var trigger = GetComponent<AttackTrigger>();
        if (trigger == null)
        {
            Debug.LogWarning("Trigger is not attack trigger!");
            return;
        }
        trigger.Damage = damage;
        trigger.PushBackForce = pushBackForce;
        trigger.AffectedTags.Add("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

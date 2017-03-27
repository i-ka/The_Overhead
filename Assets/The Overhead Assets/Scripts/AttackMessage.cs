using UnityEngine;

public class AttackMessage
{

    public int Damage { get; private set; }
    public Vector2 PushBackForce { get; private set; }

    public AttackMessage(int damage, Vector2 pushBackVector)
    {
        Damage = damage;
        PushBackForce = pushBackVector;
    }
}

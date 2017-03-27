using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMoveController))]
public class MeleeAIController : MonoBehaviour
{
    private Transform player;

    private bool rHavePlatform;
    private bool lHavePlatform;

    private Transform rPlatformCheck;
    private Transform lPlatformCheck;

    private int direction = 1;
    private bool isActivated;
    private bool isGoing;
    private CharacterMoveController moveController;
    private bool isAttack = false;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveController = GetComponent<CharacterMoveController>();
        lPlatformCheck = transform.FindChild("LPlatformCheck");
        rPlatformCheck = transform.FindChild("RPlatformCheck");
        StartCoroutine("checkRange");
    }

    IEnumerator checkRange()
    {
        for (;;)
        {
            Vector3 distance = player.position - transform.position;

            float sqrLen = distance.sqrMagnitude;
            isActivated = sqrLen <= 1000;
            isGoing = sqrLen <= 100;
            isAttack = sqrLen <= 17;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Patrol()
    {
		if (!moveController.Grounded) {
			return;
		}
        if (!rHavePlatform || !lHavePlatform)
        {
            direction *= -1;
        }
        moveController.Move(direction, false);
    }

    void GoingTo()
    {
        moveController.Move((player.position.x - transform.position.x) / Mathf.Abs(player.position.x - transform.position.x), false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            moveController.Attack(isAttack);
        }
        else if (isGoing)
        {
            GoingTo();
        }
        else if (isActivated)
        {
            Patrol();
        } else {
            moveController.Move(0, false);
        }
    }
    void FixedUpdate()
    {
        if(!isActivated) {
            return;
        }
        float checkRadius = 0.2f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(lPlatformCheck.position, checkRadius, moveController.Ground);
        lHavePlatform = colliders.Length > 0;
        colliders = Physics2D.OverlapCircleAll(rPlatformCheck.position, checkRadius, moveController.Ground);
        rHavePlatform = colliders.Length > 0;
    }
}


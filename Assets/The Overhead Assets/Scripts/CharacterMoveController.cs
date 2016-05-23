using UnityEngine;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {

    private bool facingRight=true;
    private bool grounded;
    private float k_grounded_radius = 0.1f;
    private Transform m_ground_check;
    private Rigidbody2D m_rb;
    private Animator m_anim;
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackTime = 0.1f;
    [HideInInspector]
    public StatManager m_stats;
    private Transform m_atkTrigger;


    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    public LayerMask ground;
    public string m_enemyTag="Enemy";


	void Start () {
        m_stats = GetComponent<StatManager>();
        m_atkTrigger = transform.FindChild("AttackTrigger");
        m_atkTrigger.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_ground_check = transform.Find("GroundCheck");
	}

    public void Attack(bool atk)
    {
        if (!attacking && atk && attackTimer >= m_stats.attackCoolDown) {
            attacking = true;
            attackTimer = 0;
        } else if (attacking && attackTimer >= attackTime) {
            attacking = false;
        }
        if (attackTimer <= m_stats.attackCoolDown) {
            attackTimer += Time.deltaTime;
        }
        m_anim.SetBool("Attack",attacking);
        m_atkTrigger.gameObject.SetActive(attacking);
    }

    void FixedUpdate()
    {
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_ground_check.position, k_grounded_radius, ground);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        m_anim.SetBool("Grounded", grounded);
    }

    public void Move(float axis,bool jump)
    {
        float moveSpeed = axis * maxSpeed;
        m_anim.SetFloat("Speed",Mathf.Abs(moveSpeed));
        m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);

        if(jump && grounded)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpPower);
        }

        if (axis<0 && facingRight)Flip();
        if (axis > 0 && !facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale=transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void Update()
    {
        if (m_stats.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(StatManager))]
public class CharacterMoveController : MonoBehaviour {

    public GameObject hitDamage;
    
    public StatManager Stats { get { return m_stats; } }
    public LayerMask Ground { get { return m_ground; } }
    public string EnemyTag { get { return m_enemyTag; } }
    public bool Grounded { get { return m_grounded; } }

    [SerializeField]
    private bool m_grounded;
    [SerializeField]
    private string m_enemyTag = "Enemy";
    [SerializeField]
    private LayerMask m_ground;
    private StatManager m_stats;
    private Transform m_ground_check;
    private Rigidbody2D m_rb;
    private Animator m_anim;
    private bool m_attacking = false;
    private float m_attackTime = 0.1f;
    private Transform m_atkTrigger;
    private AudioSource m_moveSound;
    private AudioSource m_attackSound;
    private bool m_controlsEnable;
    private bool m_facingRight = true;
    private float k_grounded_radius = 0.1f;

	void Start () {
        m_stats = GetComponent<StatManager>();
        m_atkTrigger = transform.FindChild("AttackTrigger");
        m_atkTrigger.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_ground_check = transform.Find("GroundCheck");
		AudioSource[] allSources = GetComponents<AudioSource>();
        m_moveSound = allSources[0];
        m_attackSound = allSources[1];
        m_controlsEnable = true;
    }

    public void Attack(bool atk)
    {
        if (m_attacking || !atk)
        {
            return; 
        }
        StartCoroutine(performAttack());
    }

    void FixedUpdate()
    {
        m_grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_ground_check.position, k_grounded_radius, m_ground);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_grounded = true;
            }
        }
        m_anim.SetBool("Grounded", m_grounded);
    }

    public void Move(float axis,bool jump)
    {
        if (!m_controlsEnable) {
            return;
        }
        
        float moveSpeed = axis * m_stats.maxSpeed;
        m_anim.SetFloat("Speed",Mathf.Abs(moveSpeed));
        m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);

        if(jump && m_grounded)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_stats.jumpPower);
        }

        if (axis<0 && m_facingRight)Flip();
        if (axis > 0 && !m_facingRight) Flip();

        if (m_grounded && moveSpeed != 0 && !m_moveSound.isPlaying)
        {
            m_moveSound.volume = Random.Range(0.8f, 1);
            m_moveSound.pitch = Random.Range(0.8f, 1.2f);
            m_moveSound.Play();
        }
    }

    void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 scale=transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void Update()
    {
        if (!m_stats.isAlive && !gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(int damage)
    {
        m_stats.takeDamage(damage);
        Instantiate(hitDamage, transform.position, transform.rotation);
    }

    public void pushBack(Vector2 force)
    {
        StartCoroutine("dissableControls");
        m_rb.velocity = new Vector2(0, 0);
        m_rb.AddForce(force);
    }
    IEnumerator dissableControls()
    {
        m_controlsEnable = false;
        yield return new WaitForSeconds(1);
        m_controlsEnable = true;
    }

    IEnumerator performAttack()
    {
        m_attacking = true;
        m_anim.SetBool("Attack", true);
        m_atkTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(m_attackTime);
        m_anim.SetBool("Attack", false);
        m_atkTrigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(m_stats.attackCoolDown);
        m_attacking = false;
    }
}

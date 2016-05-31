using UnityEngine;
using System.Collections;
[RequireComponent(typeof(StatManager))]
public class CharacterMoveController : MonoBehaviour {

    private bool facingRight=true;
    private bool grounded;
    private float k_grounded_radius = 0.1f;
    public GameObject hitDamage;
    private Transform m_ground_check;
    private Rigidbody2D m_rb;
    private Animator m_anim;
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackTime = 0.1f;
    private Transform m_atkTrigger;
    private AudioSource moveSound;
    private AudioSource attackSound;
    private bool controlsEnable;

    [HideInInspector]
    public StatManager stats;
    public LayerMask ground;
    public string m_enemyTag="Enemy";
	public bool grounded;

	void Start () {
        stats = GetComponent<StatManager>();
        m_atkTrigger = transform.FindChild("AttackTrigger");
        m_atkTrigger.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_ground_check = transform.Find("GroundCheck");
		AudioSource[] allSources = GetComponents<AudioSource>();
        moveSound = allSources[0];
        attackSound = allSources[1];
        controlsEnable = true;
    }

    public void Attack(bool atk)
    {
        if (!attacking && atk && attackTimer >= stats.attackCoolDown) {
            attacking = true;
            attackTimer = 0;
            if (attacking && !attackSound.isPlaying)
            {
                attackSound.volume = Random.Range(0.8f, 1);
                attackSound.Play();
            }
        } else if (attacking && attackTimer >= attackTime) {
            attacking = false;
        }
        if (attackTimer <= stats.attackCoolDown) {
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
        if (!controlsEnable) {
            return;
        }
        
        float moveSpeed = axis * stats.maxSpeed;
        m_anim.SetFloat("Speed",Mathf.Abs(moveSpeed));
        m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);

        if(jump && grounded)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, stats.jumpPower);
        }

        if (axis<0 && facingRight)Flip();
        if (axis > 0 && !facingRight) Flip();

        if (grounded && moveSpeed != 0 && !moveSound.isPlaying)
        {
            moveSound.volume = Random.Range(0.8f, 1);
            moveSound.pitch = Random.Range(0.8f, 1.2f);
            moveSound.Play();
        }
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
        if (!stats.isAlive && !gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(int damage)
    {
        stats.takeDamage(damage);
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
        controlsEnable = false;
        yield return new WaitForSeconds(1);
        controlsEnable = true;
    }
}

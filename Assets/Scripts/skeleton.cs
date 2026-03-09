using UnityEngine;

public class skeleton : MonoBehaviour
{
    [Header("Rangos")]
    public float visionRange = 10f;
    public float attackRange = 1.8f;
    public float moveSpeed = 2f;

    [Header("Atributos")]
    public float health = 50f;
    public int damage = 10;

    [Header("Ataque")]
    public float attackCooldown = 1.2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    private Animator anim;
    private Rigidbody2D rb;
    private Transform player;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Atacar();
        }
        else if (distance <= visionRange && !isAttacking)
        {
            Perseguir();
        }
        else if (!isAttacking)
        {
            IrAIdle();
        }
    }

    void Perseguir()
    {
        anim.SetBool("isRunning", true);

        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        transform.localScale = new Vector3(dir, 1, 1);
    }

    void Atacar()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);

        // mirar al jugador antes de atacar
        float dir = Mathf.Sign(player.position.x - transform.position.x);
        if (dir != 0) transform.localScale = new Vector3(dir, 1, 1);

        if (attackTimer <= 0f && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            anim.SetTrigger("attack");
            Debug.Log("Skeleton intenta atacar");
        }
    }

    void IrAIdle()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);
    }

    // ESTE MÉTODO DEBE SER LLAMADO POR UN ANIMATION EVENT
    public void GolpeHacha()
    {
        Debug.Log("GolpeHacha se ejecutó");

        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= attackRange + 0.5f)
        {
            playerController pc = player.GetComponent<playerController>();
            if (pc != null)
            {
                pc.TakeDamage(damage);
                Debug.Log("Daño aplicado al player");
            }
            else
            {
                Debug.LogWarning("No se encontró playerController en el player");
            }
        }
    }

    // ESTE MÉTODO DEBE SER LLAMADO AL FINAL DE LA ANIMACIÓN DE ATAQUE
    public void FinAtaque()
    {
        isAttacking = false;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 3f);
    }
}
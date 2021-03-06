using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public Transform attackPoint;
    public Transform blockPoint;
    public Transform feet;
    
    public HealthBar healthBar;

    public BoxCollider2D boxCollider2D;

    public LayerMask enemyLayers;
    public LayerMask groundLayer;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float blockRange;
    [SerializeField] private float knockback;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumps;
    [SerializeField] private int remainingJumps;


    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    private float horizontal;
    private bool block;
    private float nextAttackTime = 0f;
    private bool dead = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);

        remainingJumps = maxJumps;
    }

    private void Update()
    {
        
        //Tiempo de enfriamiento para volver a atacar
        
        if (Time.time >= nextAttackTime) 
        {
            if (Input.GetButtonDown("Fire1") && !block)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //Bloquear

        if(!dead){
        
        if (Input.GetButton("Fire2") && IsGrounded())
        {
            Block();
        }
        else
        {
            block = false;
        }
        }

        //Rotar personaje

        if (horizontal > 0.0f) //Derecha
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); //Rota el personaje
        }
        else if (horizontal < 0.0f) //Izquierda
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); //Rota el personaje
        }

        //Comprobar si esta saltando,cayendo o en el suelo

        if (rb.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }

        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            remainingJumps = maxJumps;
        }



        //Salto y doble salto

        if(!block)
        {
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump") && !IsGrounded() && remainingJumps > 0)
        {
            DoubleJump();
        }
        
        }

        //Movimiento

        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isRunning", horizontal != 0.0f);

        //Dialogo        

        if (dialogueUI.IsOpen) return;

        if (Input.GetKeyDown(KeyCode.E) && dialogueUI.IsOpen == false)
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
        
        //Muerte al caer

        if (rb.position.y < -30 && !dead)
        {
           Die(0f);
        }

    }

//---------------------------------------------------------------------------------------------------------------//

    //En el fixedUpdate va lo relacionado con la fisica

    private void FixedUpdate()
    {
        if(!block) Move(horizontal);
    }
    
//---------------------------------------------------------------------------------------------------------------//

    //Atacar y defender

    private void Attack() //Ataca
    {
        animator.SetTrigger("attack");

        //Hace daño a los enemigos en ese area
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Slime slime = enemy.GetComponent<Slime>();
            Bullet bullet = enemy.GetComponent<Bullet>();

            if (slime != null)
            {
                slime.TakeDamage(attackDamage, knockback, this.transform);
            }

            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
        }
    }

    private void Block()
    {
        animator.SetTrigger("isBlocking");
        rb.velocity = new Vector2(0, 0);
        block = true;

        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(blockPoint.position, blockRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            int direction = transform.position.x < enemy.GetComponent<Transform>().position.x ? 1 : -1;

            Slime slime = enemy.GetComponent<Slime>();
            Bullet bullet = enemy.GetComponent<Bullet>();

            if (slime != null)
            {
                slime.Knock(direction, knockback);
            }

            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
            if(enemy != null)
            {
                FindObjectOfType<AudioManager>().PlaySound("Block");
            }
        }
    }

    //Ver rango de ataque y escudo

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)

            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        if (blockPoint == null)

            return;

        Gizmos.DrawWireSphere(blockPoint.position, blockRange);
    }

//---------------------------------------------------------------------------------------------------------------//

    //Movimiento

    private void Jump()
    {
        FindObjectOfType<AudioManager>().PlaySound("Jump");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void DoubleJump()
    {
        FindObjectOfType<AudioManager>().PlaySound("Jump");
        rb.velocity = new Vector2(transform.position.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        remainingJumps--;
    }

    private void Move(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }


//---------------------------------------------------------------------------------------------------------------//

    //Recibir daño,curarse y Morir

    public void TakeDamage(int damage,int knock,int direction)
    {
        if(!dead){
            FindObjectOfType<AudioManager>().PlaySound("Hurt");
            currentHealth -= damage;
            animator.SetTrigger("hurt");
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die(3f);
            }
            StartCoroutine(Knock(direction, knock));
        }
    }
    
    public IEnumerator Knock(int direction,float knockback)
    {

        float timer = 0;

        while (0.02f > timer)
        {
            Debug.Log(direction);
            timer += Time.deltaTime;
            rb.AddForce(new Vector2(knockback * direction, .1f));
        }

        yield return 0;
        
    }

    public void RestoreHealth(int restoreValue)
    {
        currentHealth += restoreValue;
        healthBar.SetHealth(currentHealth);
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die(float restartDelay)
    {
        FindObjectOfType<AudioManager>().PlaySound("Die");
        animator.SetBool("isDead", true);
        dead = true;
        Invoke("GameOver",restartDelay);
    }

    private void GameOver()
    {
        FindObjectOfType<GameManager>().GameOver();
    }

//---------------------------------------------------------------------------------------------------------------//      
    
    //Colisiones

    private void OnCollisionEnter2D(Collision2D other)
    {

    if (other.collider.CompareTag("Ground"))
    {
      
    }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
    if (other.collider.CompareTag("Ground") && dead)
    {
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
       
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.2f, groundLayer);

        if (groundCheck != null)
        {
            return true;
        }
        else
            {
                return false;
            }
    }

    //---------------------------------------------------------------------------------------------------------------//    

}

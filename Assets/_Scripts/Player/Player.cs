using System;
using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator animator;

        public Transform attackPoint;
        public Transform blockPoint;

        public HealthBar healthBar;
        public Canvas canvas;
        
        public BoxCollider2D boxCollider2D;

        public LayerMask enemyLayers;
        public LayerMask groundLayer;

        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        
        [SerializeField] private float attackRange;
        [SerializeField] private float blockRange;
        [SerializeField] private int attackDamage;
        [SerializeField] private float attackRate;
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
        private bool isGrounded;


        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(currentHealth);
            
            remainingJumps = maxJumps;
        }

        private void Update()
        {
            if (Time.time >= nextAttackTime) //Tiempo de enfriamiento para volver a atacar
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }

            if (Input.GetButton("Fire2") && isGrounded)
            {
                Block();
            }
            else
            {
                block = false;
            }

//---------------------------------------------------------------------------------------------------------------//

            if (horizontal > 0.0f) //Derecha
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); //Rota el personaje
            }
            else if (horizontal < 0.0f) //Izquierda
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); //Rota el personaje
            }

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

            if (isGrounded)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                remainingJumps = maxJumps;
            }

            animator.SetBool("isRunning", horizontal != 0.0f); //Parar o ejecutar animacion de correr

            if (Input.GetButtonDown("Jump") && isGrounded) //Si aprieto saltar y tengo saltos restantes
            {
                Jump();
            }

            if (Input.GetButtonDown("Jump") && !isGrounded && remainingJumps > 0)
            {
                DoubleJump();
            }

//---------------------------------------------------------------------------------------------------------------//

            //Dialogo        

            if (dialogueUI.IsOpen) return;

            if (Input.GetKeyDown(KeyCode.E) && dialogueUI.IsOpen == false)
            {
                if (Interactable != null)
                {
                    Interactable.Interact(this);
                }
            }

        }

//---------------------------------------------------------------------------------------------------------------//

        private void FixedUpdate()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if(!block) Move(horizontal);
        }
        
//---------------------------------------------------------------------------------------------------------------//

        //Colisionadores

        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if(other.collider.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if(other.collider.CompareTag("Ground") && dead)
            {
                rb.velocity = new Vector2(0, 0);
                rb.isKinematic = true;
                GetComponent<BoxCollider2D>().enabled = false;
               
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            isGrounded = false;
        }

        //---------------------------------------------------------------------------------------------------------------//

        //Recibir daño y Morir
    
        public void TakeDamage(int damage)
        {

                currentHealth -= damage;
                animator.SetTrigger("hurt");
                healthBar.SetHealth(currentHealth);
                if (currentHealth <= 0)
                {
                    Die();
                }
        }
        
        private void Die()
        {
            animator.SetBool("isDead", true);
            dead = true;
        }
        
//---------------------------------------------------------------------------------------------------------------//

        //Atacar y defender
    
        private void Attack()//Ataca
        {
            animator.SetTrigger("attack");

            //Hace daño a los enemigos en ese area
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
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
            block = true;
            rb.velocity = new Vector2(0,0);
        
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(blockPoint.position, new Vector2(blockRange, 2), 0, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {

                int direction = transform.position.x < enemy.GetComponent<Transform>().position.x ? 1 : -1;
            
                Slime slime = enemy.GetComponent<Slime>();
                Bullet bullet = enemy.GetComponent<Bullet>();
            
                if(slime != null)
                {
                    slime.Knock(direction, 2);
                }
            
                if(bullet != null)
                {
                    Destroy(bullet.gameObject);
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

            Gizmos.DrawWireCube(blockPoint.position, new Vector3(blockRange, 2, 0));
        }
        
//---------------------------------------------------------------------------------------------------------------//

        //Movimiento

        private void Jump()
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        private void DoubleJump()
        {
            rb.velocity = new Vector2(transform.position.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            remainingJumps--;
        }
        
        private void Move(float horizontal)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
//---------------------------------------------------------------------------------------------------------------//
        
        
        
        
        
        
        
        /*
        public bool IsGrounded()//Lanza una caja desde el centro del personaje y comprueba si esta chocando con los elementos en la capa "suelo"
        {
            RaycastHit2D raycastHit2D =  Physics2D.BoxCast(boxCollider2D.bounds.center, new Vector2(boxCollider2D.bounds.size.x, boxCollider2D.size.y), 0f, Vector2.down, 0.5f,groundLayer);
            return raycastHit2D.collider != null;
        }
        */
        
        
        
    }
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
   
    public Animator animator;
    public Rigidbody2D rb;

    public HealthBar healthBar;
    public Canvas canvas;

    private bool dead = false;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;
    [SerializeField] public int attackDamage = 20;
    [SerializeField] public int knockback = 5;
    
    [SerializeField] private float speed;
    [SerializeField] private bool MoveRight;
    [SerializeField] private bool moving;
    [SerializeField] private float agroSpeed;
    [SerializeField] private float jumpPower;
    
    [SerializeField] private GameObject player;
    private GameObject spawnedPlayer;
    
    private int direction;

    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider2D;

    public GameObject blood;


    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spawnedPlayer = FindObjectOfType<Player>().gameObject;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
        
        moving = true;
    }

    void Update()
    {
        //Movimiento y ataque
        if (moving)
        {
            move();
        }
        else
        {
            idle();
        }
    }
    
    private void FixedUpdate()
    {
        if (spawnedPlayer == null) return;
        direction = this.transform.position.x < spawnedPlayer.transform.position.x ? 1 : -1;

        float distance = Mathf.Abs(spawnedPlayer.transform.position.x - transform.position.x);

        if (isGrounded() && distance < 6.0f)//Si esta en el suelo y el personaje esta cerca ataca
        {
            attack(direction);
        }

        if(distance < 7.0f && distance > 6.0f)//Si estoy entre 7 y 6 esta en alerta
        {
            animator.SetTrigger("isAlert");
            moving = false;
        }
        if(distance > 7.0f)//Se olvida
        {
            moving = true;
        }
    }
    
    public bool isGrounded()//Lanza una caja desde el centro del personaje y comprueba si esta chocando con los elementos en la capa "suelo"
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, new Vector2(boxCollider2D.bounds.size.x, boxCollider2D.size.y), 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }
    
    public void attack(int direction)//Empieza a atacar
    {
        animator.SetBool("isMoving", true);
        transform.localScale = new Vector2(direction, transform.localScale.y);
        rb.velocity = (new Vector2(agroSpeed * direction, jumpPower));
    }

    public void move()//Se mueve 
    {
        animator.SetBool("isMoving", true);
        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }

    public void idle()//Esta quieto cuando le detectan
    {
        animator.SetBool("isMoving", false);
        transform.Translate(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)//Si toca una barrera se da la vuelta
    {
        if (other.tag == "Turn")
        {
            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }
    }
    
    public void TakeDamage(int damage, float knockback, Transform player)
    {
        if (!dead)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            animator.SetTrigger("hurt");
            Instantiate(blood,transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
            if (currentHealth <= 0)
            {
                Die();
                dead = true;
            }

            int direction = transform.position.x < spawnedPlayer.transform.position.x ? 1 : -1;

            Knock(-direction, knockback);
        }
    }

    public void Knock(int direction,float knockback)
    {
        rb.AddForce(new Vector2(knockback * direction, 0.1f), ForceMode2D.Impulse);
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        canvas.enabled = false;
        this.enabled = false;
        //Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            spawnedPlayer.GetComponent<Player>().TakeDamage(attackDamage,knockback,direction);
        }
        if (other.collider.CompareTag("Ground") && dead)
        {
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

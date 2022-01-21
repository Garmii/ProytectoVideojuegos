using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerCombat : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Animator animator;

    public Transform attackPoint;
    public Transform blockPoint;

    public HealthBar healthBar;
    public Canvas canvas;
    public BoxCollider2D boxCollider2D;

    public LayerMask enemyLayers;

    public int maxHealth;
    public int currentHealth;
    public float attackRange;
    public float blockRange;
    public int attackDamage;
    public float attackRate;
    public float knockback;

    public bool block;
    private float nextAttackTime = 0f;
    private bool dead = false;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)//Tiempo de enfriamiento para volver a atacar
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        if (Input.GetButton("Fire2") && GetComponent<PlayerMovement>().IsGrounded())
        {
            Block();
        }
        else
        {
            block = false;
        }
    }

    private void FixedUpdate()
    {

    }

    private void Block()
    {
        animator.SetTrigger("isBlocking");
        block = true;
        playerRb.velocity = new Vector2(0,0);
        
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(blockPoint.position, new Vector2(blockRange, 2), 0, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            int direction = transform.position.x < enemy.GetComponent<Transform>().position.x ? 1 : -1;
            
            SlimeCombat slimeCombat = enemy.GetComponent<SlimeCombat>();
            Bullet bullet = enemy.GetComponent<Bullet>();
            
            if(slimeCombat != null)
            {
            slimeCombat.Knock(direction, 2);
            }
            
            if(bullet != null)
            {
                Destroy(bullet.gameObject);
            }
            
        }
    }

    public void TakeDamage(int damage)//Recibe daño
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
    }
    private void Die()//Muere
    {
        dead = true;
        animator.SetBool("isDead", true);
        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;
    }

    private void Attack()//Ataca
    {
        animator.SetTrigger("attack");

        //Hace daño a los enemigos en ese area
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            SlimeCombat slimeCombat = enemy.GetComponent<SlimeCombat>();
            Bullet bullet = enemy.GetComponent<Bullet>();
            if (slimeCombat != null)
            {
                slimeCombat.TakeDamage(attackDamage, knockback, this.transform);
            }
            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
        }
    }

    //---------------------------------------------------------------------------------------------------------------//
    private void OnDrawGizmosSelected()//Ver rango de ataque y escudo
    {
        if (attackPoint == null)

            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        if (blockPoint == null)

            return;

        Gizmos.DrawWireCube(blockPoint.position, new Vector3(blockRange, 2, 0));
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.collider.CompareTag("Ground") && dead)
        {
            playerRb.velocity = new Vector2(0, 0);
            playerRb.isKinematic = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
*/
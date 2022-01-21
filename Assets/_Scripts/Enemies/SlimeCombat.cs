using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCombat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 20;
    public int knockback = 5;

    public Animator animator;
    public Rigidbody2D rb;

    public HealthBar healthBar;
    public Canvas canvas;

    public Player playerCombat;
    public LayerMask groundLayer;

    private bool dead = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void TakeDamage(int damage, float knockback, Transform player)
    {
        if (!dead)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            animator.SetTrigger("hurt");
        if (currentHealth <= 0)
        {
            Die();
                dead = true;
        }

        int direction = transform.position.x < player.position.x ? 1 : -1;

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
        GetComponent<SlimeMovement>().enabled = false;
        canvas.enabled = false;
        this.enabled = false;
        //Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            playerCombat.TakeDamage(attackDamage);
        }
        if (other.collider.CompareTag("Ground") && dead)
        {
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

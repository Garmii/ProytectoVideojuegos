using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage,0,0);
        }

        Destroy(gameObject);
    }

}

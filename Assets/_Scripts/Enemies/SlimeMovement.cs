using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    public float speed;
    public bool MoveRight;
    public bool moving;
    public float agroSpeed;
    public float jumpPower;


    public Rigidbody2D rb;
    public GameObject player;
    public Animator animator;
    private int direction;

    public LayerMask groundLayer;
    private BoxCollider2D boxCollider2D;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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

        direction = this.transform.position.x < player.transform.position.x ? 1 : -1;

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

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
}

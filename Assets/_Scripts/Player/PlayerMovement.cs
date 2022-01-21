using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    public Rigidbody2D playerRb;
    public Animator animator;

    public LayerMask groundLayer;


    private BoxCollider2D boxCollider2D;
    private float horizontal;

    public float speed;
    public float jumpForce;
    public int saltosMaximos;
    public int saltosRestantes;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
    }


    void Update()
    {
        //---------------------------------------------------------------------------------------------------------------//

        if(horizontal > 0.0f)//Derecha
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);//Rota el personaje
        }
        else if (horizontal < 0.0f) //Izquierda
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); //Rota el personaje
        }

        if (playerRb.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }

        if(playerRb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }

        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            saltosRestantes = saltosMaximos;
        }

        animator.SetBool("isRunning", horizontal != 0.0f);//Parar o ejecutar animacion de correr

        if (Input.GetButtonDown("Jump") && IsGrounded())//Si aprieto saltar y tengo saltos restantes
        {
            Jump();
        }
        if (Input.GetButtonDown("Jump") && !IsGrounded() && saltosRestantes > 0)
        {
            DoubleJump();
        }

        //---------------------------------------------------------------------------------------------------------------//
        if (dialogueUI.IsOpen) return;

        if (Input.GetKeyDown(KeyCode.E) && dialogueUI.IsOpen == false)
        {
            if(Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
        //---------------------------------------------------------------------------------------------------------------//
    }

    public bool IsGrounded()//Lanza una caja desde el centro del personaje y comprueba si esta chocando con los elementos en la capa "suelo"
    {
       RaycastHit2D raycastHit2D =  Physics2D.BoxCast(boxCollider2D.bounds.center, new Vector2(boxCollider2D.bounds.size.x, boxCollider2D.size.y), 0f, Vector2.down, 0.5f,groundLayer);
        return raycastHit2D.collider != null;
    }


    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");//Entrada de usuario
        if(!GetComponent<PlayerCombat>().block) Move(horizontal);
    }

    private void Jump()
    {
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void DoubleJump()
    {
        playerRb.velocity = new Vector2(transform.position.x, 0);
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        saltosRestantes--;
    }


    private void Move(float horizontal)
    {
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
    }

}
*/
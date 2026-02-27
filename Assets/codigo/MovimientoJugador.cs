using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float velocidad = 5f;
    public float jumpForce = 6f; // Fuerza del salto
    public float rayLength = 0.6f; // Longitud del rayo para detectar el suelo
    public LayerMask groundLayer; // Capa del suelo para deteccion

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    

    void Update()
    //Velocidad del movimiento horizontal, multiplicada por el
    //tiempo para que sea consistente en diferentes velocidades de fotogramas    
    {
        float moveInput = Input.GetAxis("Horizontal");

    rb.linearVelocity = new Vector2(moveInput * velocidad, rb.linearVelocity.y);

    // Animación
    animator.SetFloat("movement", Mathf.Abs(moveInput));

    // Giro 
    if (moveInput > 0) transform.localScale = new Vector3(6.0518f, 5.6918f, 1f);
    else if (moveInput < 0) transform.localScale = new Vector3(-6.0518f, 5.6918f, 1f);

        //Salto del personaje, se activa al presionar la barra espaciadora
        //y solo si el personaje esta en el suelo
       
        rb.linearVelocity = new Vector2(moveInput * velocidad, rb.linearVelocity.y);

        isGrounded = IsGrounded();

        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }
    
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        UnityEngine.Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);

        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }

}


using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 6f; // Fuerza del salto
    public float rayLength = 0.33f; // Longitud del rayo para detectar el suelo
    public LayerMask groundLayer; // Capa del suelo para detecci n

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;
    public Animator animator;


    public TextMeshProUGUI objectCounterText;

    private Dictionary<string, int> collectedObjects = new Dictionary<string, int>()
    {
        { "moneda", 0 }
    };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateObjectCounterUI();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        
        isGrounded = IsGrounded();



        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();


        //animator.SetFloat("movimiento", moveInput * speed);  
        animator.SetBool("saltando", !isGrounded);
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
        animator.SetFloat("movimiento", Mathf.Abs(moveInput));

        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            string objectName = collision.gameObject.name;

            if (collectedObjects.ContainsKey(objectName))
            {
                collectedObjects[objectName]++;
                UpdateObjectCounterUI();
            }
            Destroy(collision.gameObject);
        }
    }

    void UpdateObjectCounterUI()
    {
        objectCounterText.text = $"moneda: {collectedObjects["moneda"]}";
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

   [Header("Movimiento Horizontal")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Mecánicas de Salto")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float fallMultiplier = 2.5f;      // Caída rápida y pesada
    [SerializeField] private float lowJumpMultiplier = 2f;    // Salto corto si sueltas rápido el botón

    [Header("Detección de Suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Gamer Feel (Jugabilidad)")]
    [SerializeField] private float coyoteTime = 0.15f;         
    private float coyoteTimeCounter;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;
    
    // LIMITADOR ESTRICTO PARA EVITAR SALTO INFINITO
    private bool canJump; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Entrada de movimiento horizontal
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Detección física del suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 3. Lógica del Tiempo Coyote y Limitador
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Resetea el tiempo en el suelo
            canJump = true;                 // ¡LIMITADOR!: Solo en el suelo o en Coyote Time se permite saltar
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Disminuye el tiempo en el aire
            
            // Si se acaba el Coyote Time en el aire, se bloquea el salto por completo
            if (coyoteTimeCounter <= 0f)
            {
                canJump = false; 
            }
        }

        // 4. Entrada del salto (Verifica el botón Y el limitador canJump)
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            // ¡LIMITADOR ACTIVO!: Inmediatamente se vuelve falso para no permitir otro salto en el aire
            canJump = false; 
            coyoteTimeCounter = 0f; 
        }

        // 5. Control de orientación del sprite
        FlipController();
    }

    void FixedUpdate()
    {
        // 6. Aplicar velocidad horizontal
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // 7. Modificadores de Gravedad (Evita que el personaje flote)
        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void FlipController()
    {
        if (horizontalInput > 0 && !isFacingRight || horizontalInput < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}



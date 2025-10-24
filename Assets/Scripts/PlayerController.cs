using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Velocidad de desplazamiento del jugador")]
    public float speed = 5f;

    [Header("Referencias")]
    [Tooltip("Rigidbody2D del jugador")]
    public Rigidbody2D rb;

    [Tooltip("Animator del jugador")]
    public Animator animator;

    private Vector2 movement;
    private bool canMove = true;

    void Update()
    {
        if (canMove)
        {
            Movimiento();
        }

        // --- Interacción ---
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaccion();
        }
    }

    void FixedUpdate()
    {
        // --- Movimiento físico ---
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    public void Movimiento()
    {
        // --- Captura de entrada ---
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // --- Normalización (para que no se mueva más rápido en diagonal) ---
        movement = movement.normalized;

        // --- Enviar los valores al Animator (Blend Tree) ---
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);

    }

    public void Interaccion()
    {
        // Aquí va la lógica de interacción con objetos
        Debug.Log("Interacción realizada");

        // Dispara una animación (por ejemplo, levantar algo o saludar)
        animator.SetTrigger("Accion");

       
         StartCoroutine(BloquearMovimientoPorAnimacion());
    }

    private IEnumerator BloquearMovimientoPorAnimacion()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f); // Duración de la animación
        canMove = true;
    }
    
}

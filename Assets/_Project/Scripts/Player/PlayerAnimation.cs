using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerController player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();

    }
    private void Update()
    {
        // Warning: Tres strings mágicos por frame ("Speed", "VelocityY", "IsGrounded") que tienen que coincidir con Player.controller.
        // Definilos como static readonly int con Animator.StringToHash para no hashear el string cada frame y evitar errores de tipeo.
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", player.IsGrounded);
    }

}

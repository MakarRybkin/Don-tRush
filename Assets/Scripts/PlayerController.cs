using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float horizontalInput;
    [SerializeField] private float movingSpeed;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundMask;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    void FixedUpdate()
    {
        Moving();
    }
    private void Update()
    {
        if (isGrounded() && Input.GetButtonDown("Jump") )
            Jump();
        animator.SetBool("IsFlying", rigidbody.velocity.y < 0 ? true : false) ;
    }
    private void Moving()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * movingSpeed);
        if(horizontalInput!=0)
            playerSprite.flipX = horizontalInput <= 0 ;
        animator.SetBool("IsMoving", horizontalInput != 0 );
    }
    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse) ;
        animator.SetTrigger("Jump");
    }
    private bool isGrounded()
    {
        var raycast = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down* capsuleCollider.bounds.size, 0.5f, groundMask);
        return raycast.collider != null;
    }
}

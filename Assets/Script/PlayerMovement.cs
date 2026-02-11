using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayerMovement : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource footstepAudio;


    public Animator animator;

    public float moveSpeed = 5f; // Movement speed

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0;
        }
        else
        {
            horizontal = 0;
        }

        moveInput = new Vector2(horizontal, vertical);

        // Animator
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        if (moveInput != Vector2.zero)
        {
            if (footstepAudio != null && !footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio != null && footstepAudio.isPlaying)
                footstepAudio.Stop();
        }
    }



    void FixedUpdate()
    {
        // Apply movement
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}

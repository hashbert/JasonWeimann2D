using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce =  200f;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();
        if (Mathf.Abs(horizontal) >= 1)
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
        }
        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);

        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }
    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}

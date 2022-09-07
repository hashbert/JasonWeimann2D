using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce =  200f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private Transform _feet;

    private Vector3 _startPosition;
    private int _jumpsRemaining;

    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
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
        
        if (Input.GetButtonDown("Fire1") && _jumpsRemaining > 0)
        {
            rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _jumpsRemaining -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default"));
        if (hit != null)
        {
            _jumpsRemaining = _maxJumps;
        }
    }
    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}

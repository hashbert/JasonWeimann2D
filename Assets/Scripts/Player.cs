using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpVelocity = 10f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private Transform _feet;
    [SerializeField] private float _downPull = 5f;
    [SerializeField] private float _maxJumpDuration = 0.1f;

    private Vector3 _startPosition;
    private int _jumpsRemaining;
    private float _fallTimer;
    private float _jumpTimer;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _horizontal;
    private bool _isGrounded;

    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        MoveHorizontal();

        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldStartJump())
            Jump();
        else if (ShouldContinueJump())
            ContinueJump();

        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
        }
    }

    private void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }

    private bool ShouldContinueJump()
    {
        return Input.GetButton("Fire1") && _jumpTimer <= _maxJumpDuration;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _jumpsRemaining--;
        Debug.Log($"Jumps remaining {_jumpsRemaining}");
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    private bool ShouldStartJump()
    {
        return Input.GetButtonDown("Fire1") && _jumpsRemaining > 0;
    }

    private void MoveHorizontal()
    {
        if (Mathf.Abs(_horizontal) >= 1)
        {
            _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        }
    }

    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * _speed;
    }

    private void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    private void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool("Walk", walking);
    }

    private void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Default"));
        _isGrounded = hit != null;
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _slipFactor = 1;
    [Header("Jump")]
    [SerializeField] private float _jumpVelocity = 10f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private Transform _feet;
    [SerializeField] private float _downPull = 5f;
    [SerializeField] private float _maxJumpDuration = 0.1f;
    [SerializeField] private int _playerNumber;

    private Vector3 _startPosition;
    private int _jumpsRemaining;
    private float _fallTimer;
    private float _jumpTimer;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private string _horizontalAxis;
    private float _horizontal;
    private bool _isGrounded;
    private bool _isOnSlipperySurface;
    private int _layerMask;
    private string _jumpButton;
    private AudioSource _audioSource;

    public int PlayerNumber => _playerNumber;
    //public int PlayerNumber { get { return _playerNumber; } } exact same as above

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _layerMask = LayerMask.GetMask("Default");
        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxis = $"P{_playerNumber}Horizontal";
    }
    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        if (_isOnSlipperySurface)
        {
            SlipHorizontal();
        }
        else
        {
            MoveHorizontal();
        }

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
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _jumpsRemaining--;
        Debug.Log($"Jumps remaining {_jumpsRemaining}");
        _fallTimer = 0;
        _jumpTimer = 0;

        if (_audioSource != null) // this needs to be done like this instead of ?
        {
            _audioSource.Play();
        }
    }

    private bool ShouldStartJump()
    {

        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    private void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }

    private void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(
            _rigidbody2D.velocity,
            desiredVelocity,
            Time.deltaTime / _slipFactor);
        _rigidbody2D.velocity = smoothedVelocity;
    }

    private void ReadHorizontalInput()
    {

        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;
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
        _animator.SetBool("Jump", ShouldContinueJump());
    }

    private void UpdateIsGrounded()
    {

        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);
        _isGrounded = hit != null;


        if (hit != null)
        {
            _isOnSlipperySurface = hit.CompareTag("Slippery");
        }
        else
        {
            _isOnSlipperySurface = false;
        }
        //_isOnSlipperySurface = hit?.CompareTag("Slippery") ?? false;   is same code as above
    }
    internal void ResetToStart()
    {
        _rigidbody2D.position = _startPosition;
        SceneManager.LoadScene("Menu");
    }
    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
}

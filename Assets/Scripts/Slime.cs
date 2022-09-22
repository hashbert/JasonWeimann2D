using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform _leftSensor;
    [SerializeField] private Transform _rightSensor;

    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private float _direction = -1f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidBody2D.velocity = new Vector2(_direction, _rigidBody2D.velocity.y);

        if (_direction < 0f)
        {
            Debug.DrawRay(_leftSensor.position, Vector2.down * .1f, Color.red);
            var leftResult = Physics2D.Raycast(_leftSensor.position, Vector2.down, 0.1f);
            if (leftResult.collider == null)
            {
                TurnAround();
            }
        }
        else if (_direction > 0f)
        {
            Debug.DrawRay(_rightSensor.position, Vector2.down * .1f, Color.red);
            var rightResult = Physics2D.Raycast(_rightSensor.position, Vector2.down, 0.1f);

            if (rightResult.collider == null)
            {
                TurnAround();
            }
        }


    }

    private void TurnAround()
    {
        _direction *= -1;
        if (_direction > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        player.ResetToStart();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, ITakeDamage
{
    [SerializeField] private Transform _leftSensor;
    [SerializeField] private Transform _rightSensor;
    [SerializeField] private Sprite _deadSprite;

    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private float _direction = -1f;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidBody2D.velocity = new Vector2(_direction, _rigidBody2D.velocity.y);

        if (_direction < 0f)
        {
            ScanSensor(_leftSensor);
        }
        else
        {
            ScanSensor(_rightSensor);
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(Die());
    }

    private void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * .1f, Color.red);
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
        {
            TurnAround();
        }

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * .1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideResult.collider != null)
        {
            TurnAround();
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
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        var contact = collision.contacts[0];
        var normal = contact.normal;

        print(normal);
        if (normal.y < -0.6f)
        {
            TakeDamage();
        }
        else
            player.ResetToStart();
    }
    private IEnumerator Die()
    {
        GetComponent<Animator>().enabled = false;
        _spriteRenderer.sprite = _deadSprite;
        _rigidBody2D.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        float alpha = 1;
        enabled = false;
        _audioSource?.Play();
        while (alpha > 0)
        {
            alpha-=Time.deltaTime;
            _spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}

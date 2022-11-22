using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _bounceVelocity = 10f;
    [SerializeField] private Sprite _downSprite;
    
    private SpriteRenderer _spriteRenderer;
    private Sprite _upSprite;

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _bounceVelocity);
            _spriteRenderer.sprite = _downSprite;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}

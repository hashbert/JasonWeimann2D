using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private UnityEvent _onPressed; 
    [SerializeField] private UnityEvent _onReleased;
    private SpriteRenderer _spriteRenderer;
    private Sprite _releasedSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
        BecomeReleased();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        BecomePressed();
    }

    private void BecomePressed()
    {
        _spriteRenderer.sprite = _pressedSprite;
        _onPressed?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        BecomeReleased();
    }

    private void BecomeReleased()
    {
        _spriteRenderer.sprite = _releasedSprite;
        _onReleased?.Invoke();
    }
}

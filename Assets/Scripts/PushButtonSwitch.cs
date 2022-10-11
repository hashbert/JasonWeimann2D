using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private UnityEvent _onPressed; 
    [SerializeField] private UnityEvent _onReleased;
    [SerializeField] private int _playerNumber = 1;
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
        if (player == null || player.PlayerNumber != _playerNumber) return;

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
        if (player == null || player.PlayerNumber != _playerNumber) return;

        BecomeReleased();
    }

    private void BecomeReleased()
    {
        if (_onReleased.GetPersistentEventCount() > 0)
        {
            _spriteRenderer.sprite = _releasedSprite;
            _onReleased?.Invoke();
        }
    }
}

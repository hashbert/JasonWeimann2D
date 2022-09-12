using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private UnityEvent _onEnter;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        _spriteRenderer.sprite = _downSprite;

        _onEnter?.Invoke();
    }
}

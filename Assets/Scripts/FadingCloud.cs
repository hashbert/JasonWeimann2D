using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingCloud : HittableFromBelow
{
    [SerializeField] private float _resetTime = 3f;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }
    protected override void Use()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(_resetTime);
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}

using System;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;
    [SerializeField] private Animator _animator;

    protected virtual bool CanUse => true;
    //protected virtual bool CanUse { get;  } = true; (same as above)
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (CanUse == false) return;
        var player = collision.collider.GetComponent<Player>();
        if (!player) { return; }

        if (collision.contacts[0].normal.y > 0)
        {
            PlayAnimation();
            Use();
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
            }
        }
    }

    private void PlayAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Use");
            print("hello");
        }
    }

    protected abstract void Use();
}
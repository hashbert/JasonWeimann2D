using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _leftSwitch;
    [SerializeField] private Sprite _rightSwitch;
    [SerializeField] private UnityEvent _onLeftSwitchPressed;
    [SerializeField] private UnityEvent _onRightSwitchPressed;
    private SpriteRenderer _spriteRenderer;
    private float _xPositionOfSwitch;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _xPositionOfSwitch = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;
        if (player.transform.position.x > _xPositionOfSwitch)
        {
            _spriteRenderer.sprite = _leftSwitch;
            _onLeftSwitchPressed.Invoke();
        }
        if (player.transform.position.x < _xPositionOfSwitch)
        {
            _spriteRenderer.sprite = _rightSwitch;
            _onRightSwitchPressed.Invoke();
        }
    }
}
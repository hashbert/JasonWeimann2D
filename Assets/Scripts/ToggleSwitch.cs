using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _leftSwitch;
    [SerializeField] private Sprite _rightSwitch;
    [SerializeField] private ToggleDirection _startingDirection = ToggleDirection.Center;
    private Sprite _centerSwitch;
    [SerializeField] private UnityEvent _onLeftSwitchPressed;
    [SerializeField] private UnityEvent _onRightSwitchPressed;
    private UnityEvent _onCenterSwitchPressed;
    private SpriteRenderer _spriteRenderer;
    private float _xPositionOfSwitch;
    private ToggleDirection _currentDirection;

    private enum ToggleDirection
    {
        Left,
        Center,
        Right,
    }

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
            SetToggleDirection(ToggleDirection.Left);

        }
        if (player.transform.position.x < _xPositionOfSwitch)
        {
            SetToggleDirection(ToggleDirection.Right);

        }
    }

    private void SetToggleDirection(ToggleDirection direction)
    {
        if (_currentDirection == direction) return;

        _currentDirection = direction;

        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _leftSwitch;
                _onLeftSwitchPressed.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _centerSwitch;
                _onCenterSwitchPressed.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _rightSwitch;
                _onRightSwitchPressed.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnValidate()
    {
        _startingDirection = _currentDirection;
    }
}
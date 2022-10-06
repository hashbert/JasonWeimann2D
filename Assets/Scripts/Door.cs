using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _openTop;
    [SerializeField] private Sprite _openMid;

    [SerializeField] private SpriteRenderer _rendererTop;
    [SerializeField] private SpriteRenderer _rendererMid;
    [SerializeField] private int _requiredCoins = 3;
    [SerializeField] private Door _exit;
    [SerializeField] private Canvas _canvas;

    private bool _open;

    // Update is called once per frame
    void Update()
    {
        if (_open == false && Coin.CoinsCollected >= _requiredCoins)
        {
            Open();
        }
    }

    [ContextMenu("OpenDoor")]
    public void Open()
    {
        _open = true;
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
        if (_canvas != null)
        {
            _canvas.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_open == false) return;
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
        }
    }
}

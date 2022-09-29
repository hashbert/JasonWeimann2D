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

    // Update is called once per frame
    void Update()
    {
        if(Coin.CoinsCollected >= _requiredCoins)
        {
            Open();
        }
    }

    [ContextMenu("OpenDoor")]
    void Open()
    {
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    [SerializeField] private int _totalCoins = 3;
    [SerializeField] private Sprite _usedSprite;
    private int _remainingcoins;

    // Start is called before the first frame update
    void Start()
    {
        _remainingcoins = _totalCoins;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (!player) { return; }

        if (collision.contacts[0].normal.y > 0 && _remainingcoins > 0)
        {
            Coin.CoinsCollected++;
            _remainingcoins--;
            if (_remainingcoins <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
            }
        }
    }
}

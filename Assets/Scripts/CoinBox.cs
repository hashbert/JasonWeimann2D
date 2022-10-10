using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] private int _totalCoins = 3;


    private int _remainingcoins;

    protected override bool CanUse => _remainingcoins > 0;

    // Start is called before the first frame update
    void Start()
    {
        _remainingcoins = _totalCoins;
    }

    protected override void Use()
    {
        base.Use();

        _remainingcoins--;
        Coin.CoinsCollected++;
    }
}

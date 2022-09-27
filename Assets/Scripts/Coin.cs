using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private static int _coinsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;
        Destroy(this.gameObject);
        _coinsCollected++;
        print(_coinsCollected);
    }
}

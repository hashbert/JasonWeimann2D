using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;
        Destroy(this.gameObject);
        CoinsCollected++;
        print(CoinsCollected);

        ScoreSystem.Add(100);
    }
}

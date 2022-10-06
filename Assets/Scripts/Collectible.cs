using System;
using UnityEngine;

public class Collectible: MonoBehaviour
{
    public static event Action<Collectible> OnPickedUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        gameObject.SetActive(false);
        OnPickedUp?.Invoke(this);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyLock _keylock;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
        }

        var keyLock = collision.GetComponent<KeyLock>();
        if (keyLock && keyLock == _keylock)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}

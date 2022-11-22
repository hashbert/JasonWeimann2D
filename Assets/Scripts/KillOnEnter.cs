﻿using UnityEngine;
public class KillOnEnter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }
}

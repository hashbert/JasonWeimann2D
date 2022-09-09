using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float _bounceVelocity = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _bounceVelocity);
        }
    }
}

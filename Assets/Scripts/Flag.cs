using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        //play flag wave
        //load new level
    }
}

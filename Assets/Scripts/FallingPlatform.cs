using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;

    private HashSet<Player> _players = new HashSet<Player>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        PlayerInside = true;

        _players.Add(player);

        StartCoroutine(WiggleAndFall());
    }

    private IEnumerator WiggleAndFall()
    {
        print("waiting to wiggle");
        yield return new WaitForSeconds(.25f);
        print("wiggling");
        yield return new WaitForSeconds(1f);
        print("falling");
        yield return new WaitForSeconds(3f);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        _players.Remove(player);

        if (_players.Count <= 0)
        {
            PlayerInside = false;
        }
    }
}

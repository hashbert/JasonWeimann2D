using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;

    private HashSet<Player> _playersInTrigger = new HashSet<Player>();
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        PlayerInside = true;

        _playersInTrigger.Add(player);
        if (_playersInTrigger.Count == 1)
        {
            StartCoroutine(WiggleAndFall());
        }
    }

    private IEnumerator WiggleAndFall()
    {
        print("waiting to wiggle");
        yield return new WaitForSeconds(.25f);
        float wiggleTimer = 0f;
        while (wiggleTimer < 1f)
        {
            float randomX = UnityEngine.Random.Range(-.05f, 0.05f);
            float randomY = UnityEngine.Random.Range(-.05f, 0.05f);
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(.005f, .01f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }
        yield return new WaitForSeconds(1f);
        print("falling");
        yield return new WaitForSeconds(3f);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count <= 0)
        {
            PlayerInside = false;
            StopCoroutine(WiggleAndFall());
        }
    }
}

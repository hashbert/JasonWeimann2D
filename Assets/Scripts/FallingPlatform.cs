using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;

    private HashSet<Player> _playersInTrigger = new HashSet<Player>();
    private Vector3 _initialPosition;
    private bool _falling;
    [Tooltip("Resets the wiggle timer when no players are on the platform")]
    [SerializeField] private bool _resetOnEmpty;
    [SerializeField] private float _fallSpeed = 9f;
    [Range(0.1f, 5f)] [SerializeField] private float _fallAfterSeconds = 3f;
    [Range(0.005f, .1f)] [SerializeField] private float _shakeX = .005f;
    [Range(0.005f, .1f)] [SerializeField] private float _shakeY = .005f;
    private float _wiggleTimer;

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
        //_wiggleTimer = 0f;
        while (_wiggleTimer < _fallAfterSeconds)
        {
            float randomX = UnityEngine.Random.Range(-_shakeX, _shakeX);
            float randomY = UnityEngine.Random.Range(-_shakeY, _shakeY);
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(.005f, .01f);
            yield return new WaitForSeconds(randomDelay);
            _wiggleTimer += randomDelay;
        }
        print("falling");
        _falling = true;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }

        float fallTimer = 0;
        while (fallTimer < 3f)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_falling) return;
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count <= 0)
        {
            PlayerInside = false;
            StopCoroutine(WiggleAndFall());

            if (_resetOnEmpty)
            {
                _wiggleTimer = 0;
            }
        }
    }
}

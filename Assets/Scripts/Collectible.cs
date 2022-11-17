using System;
using System.Collections.Generic;
using UnityEngine;

public class Collectible: MonoBehaviour
{
    public event Action OnPickedUp;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        OnPickedUp?.Invoke();
        if (_audioClips.Length > 0)
        {
            int randomClip = UnityEngine.Random.Range(0, _audioClips.Length);
            _audioSource?.PlayOneShot(_audioClips[randomClip]);
        }
        else
        {
            _audioSource?.Play();
        }
    }

}
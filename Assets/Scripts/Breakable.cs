using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() == null)
            return;
        if (collision.contacts[0].normal.y > 0)
        {
            var particleSystem = GetComponent<ParticleSystem>();
            particleSystem.Play();

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            _audioSource?.Play();
        }
    }
}

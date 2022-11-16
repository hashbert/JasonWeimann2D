using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;
    AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;
        CoinsCollected++;
        print(CoinsCollected);
        ScoreSystem.Add(100);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (_audioClips.Length > 0)
        {
            int randomClip = Random.Range(0, _audioClips.Length);
            m_AudioSource?.PlayOneShot(_audioClips[randomClip]);
        }
        else
        {
            m_AudioSource?.Play();
        }
    }
}

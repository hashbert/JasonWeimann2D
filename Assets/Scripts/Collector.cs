using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] private List<Collectible> _collectibles;
    [SerializeField] private UnityEvent _onCollectionComplete;
    private TMP_Text _remainingText;
    private int _countCollected;

    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach(var collectible in _collectibles)
        {
            collectible.OnPickedUp += ItemPickedUp;
        }
        int countRemaining = _collectibles.Count - _countCollected;
        _remainingText.SetText(countRemaining.ToString());
    }

    public void ItemPickedUp()
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;
        _remainingText.SetText(countRemaining.ToString());
        if (countRemaining > 0) return;
        print("got all the items");
        _onCollectionComplete?.Invoke();
    }
    private void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var collectible in _collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (var collectible in _collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
}

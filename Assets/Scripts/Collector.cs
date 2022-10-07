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

    // Start is called before the first frame update
    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach(var collectible in _collectibles)
        {
            collectible.SetCollector(this);
        }
        int countRemaining = _collectibles.Count - _countCollected;
        _remainingText.SetText(countRemaining.ToString());
    }

    //private void OnEnable()
    //{
    //    Collectible.OnPickedUp += CheckCollection;
    //}

    //private void CheckCollection(Collectible obj)
    //{
    //    _collectibles.Remove(obj);
    //    if (_collectibles.Count == 0)
    //    {
    //        print("collecting complete. do a thing");   
    //    }
    //}

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

}

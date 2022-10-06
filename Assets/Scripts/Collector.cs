using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] private List<Collectible> _collectibles;
    [SerializeField] private UnityEvent _onCollectionComplete;
    // Start is called before the first frame update
    void Start()
    {
        
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

    void Update()
    {
        foreach (var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled) return;
        }
        print("got all the items");
        _onCollectionComplete?.Invoke();
    }

    private void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }

}

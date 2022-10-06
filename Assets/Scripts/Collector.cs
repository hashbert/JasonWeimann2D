using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private List<Collectible> _collectibles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Collectible.OnPickedUp += CheckCollection;
    }

    private void CheckCollection(Collectible obj)
    {
        _collectibles.Remove(obj);
        if (_collectibles.Count == 0)
        {
            print("collecting complete. do a thing");   
        }
    }



    // Update is called once per frame
    //void Update()
    //{
    //    foreach(var collectible in _collectibles)
    //    {
    //        if (collectible.isActiveAndEnabled) return;
    //    }
    //    print("got all the items"); 
    //}

    private void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }

}

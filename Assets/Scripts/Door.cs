using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _openTop;
    [SerializeField] private Sprite _openMid;

    [SerializeField] private SpriteRenderer _rendererTop;
    [SerializeField] private SpriteRenderer _rendererMid;

    

    // Start is called before the first frame update
    void Start()
    {
        _openMid = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("OpenDoor")]
    void Open()
    {
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
    }
}

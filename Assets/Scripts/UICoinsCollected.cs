using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UICoinsCollected : MonoBehaviour
{
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = Coin.CoinsCollected.ToString();
    }
}

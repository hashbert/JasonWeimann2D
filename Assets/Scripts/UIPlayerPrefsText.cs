using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] private string _key;

    void OnEnable()
    {
        _text = GetComponent<TMP_Text>();
        _text.SetText("High Score: " + PlayerPrefs.GetInt(_key, 0).ToString());
    }

    [ContextMenu("Clear Key")]
    private void ClearKey()
    {
        PlayerPrefs.DeleteKey(_key);
    }

}

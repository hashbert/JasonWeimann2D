using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] private UnityEvent _onUnlock;
    public void Unlock()
    {
        print("unlocked");
        _onUnlock.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;

        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");
        StartCoroutine(LoadAfterDelay());
    }

    private IEnumerator LoadAfterDelay()
    {
        PlayerPrefs.SetInt(_sceneName + "Unlocked", 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(_sceneName);
    } 
}

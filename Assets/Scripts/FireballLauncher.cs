using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] private Fireball _fireball;
    [SerializeField] private float _fireRate= 0.25f;

    private Player _player;
    private string _fireButton;
    private string _horizontalAxis;
    private float _nextFireTime;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
        _horizontalAxis = $"P{_player.PlayerNumber}Horizontal";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(_fireButton) && Time.time >= _nextFireTime)
        {
            var horizontal = Input.GetAxis(_horizontalAxis);
            Fireball fireball = Instantiate(_fireball, transform.position, Quaternion.identity);
            fireball.Direction = horizontal >= 0 ? 1f : -1f;
            _nextFireTime = Time.time + _fireRate;
        }
    }
}

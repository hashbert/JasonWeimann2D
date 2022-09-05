using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * _horizontalSpeed;
        var rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);
    }
}

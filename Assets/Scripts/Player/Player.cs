using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SingletonMonobehaviour<Player>
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float movementSpeed = 5f;

    private Vector2 playerMovement;

    private void OnMovement(InputValue ctx)
    {
        playerMovement = ctx.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMovement * movementSpeed;
    }
}

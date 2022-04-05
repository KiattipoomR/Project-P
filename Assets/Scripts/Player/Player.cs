using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : SingletonMonobehaviour<Player>
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rb;

        [Header("Attributes")]
        [SerializeField] private float movementSpeed = 5f;

        private Vector2 _playerMovement;

        private void OnMovement(InputValue ctx)
        {
            _playerMovement = ctx.Get<Vector2>();
        }

        private void FixedUpdate()
        {
            rb.velocity = _playerMovement * movementSpeed;
        }
    }
}

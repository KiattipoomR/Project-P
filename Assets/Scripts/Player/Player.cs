using Manager;
using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : Singleton<Player>
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rb;

        [Header("Attributes")]
        [SerializeField] private float movementSpeed = 5f;

        private InputActionAsset _playerInput;
        private Vector2 _playerMovement;

        protected override void Awake()
        {
            base.Awake();
            _playerInput = GetComponent<PlayerInput>().actions;
        }

        private void OnEnable()
        {
            SetInactiveControlPlayerInput(false);
            PauseManager.OnPauseTriggered += SetInactiveControlPlayerInput;
        }

        private void OnDisable()
        {
            SetInactiveControlPlayerInput(true);
            PauseManager.OnPauseTriggered -= SetInactiveControlPlayerInput;
        }

        private void FixedUpdate()
        {
            rb.velocity = _playerMovement * movementSpeed;
        }

        private void OnMovement(InputValue ctx)
        {
            _playerMovement = ctx.Get<Vector2>();
        }

        private void SetInactiveControlPlayerInput(bool isInactive)
        {
            if (isInactive) _playerInput.Disable();
            else _playerInput.Enable();
        }
    }
}

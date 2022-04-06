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
            PauseManager.OnPauseTriggered += ControlPlayerInput;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= ControlPlayerInput;
        }

        private void FixedUpdate()
        {
            rb.velocity = _playerMovement * movementSpeed;
        }

        private void Update()
        {
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                GameManager.Instance.sceneControllerManager.ChangeScene("Farm - Prototype", transform.position);
            }
        }

        private void OnMovement(InputValue ctx)
        {
            _playerMovement = ctx.Get<Vector2>();
        }

        private void ControlPlayerInput(bool isPaused)
        {
            if (isPaused) _playerInput.Disable();
            else _playerInput.Enable();
        }
    }
}

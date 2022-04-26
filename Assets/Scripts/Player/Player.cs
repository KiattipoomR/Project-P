using Manager;
using Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : Singleton<Player>
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rb;

        [Header("Attributes")]
        [SerializeField] private float movementSpeed = 5f;

        public static UnityAction<int> OnInventoryFocusSlotChanged;
        public static UnityAction<Vector3> OnAimed;
        public static UnityAction<Vector3> OnInteracted;

        private InputActionAsset _playerInput;
        private Vector2 _playerMovement;
        private Vector3 _mousePosition;

        private UnityAction _setPlayerControlsInactive;
        private UnityAction _setPlayerControlsActive;

        protected override void Awake()
        {
            base.Awake();
            _playerInput = GetComponent<PlayerInput>().actions;
            _setPlayerControlsInactive = () => SetInactiveControlPlayerInput(true, true);
            _setPlayerControlsActive = () => SetInactiveControlPlayerInput(false, true);
        }

        private void OnEnable()
        {
            SetInactiveControlPlayerInput(false, true);
            PauseManager.OnPauseTriggered += SetInactiveControlPlayerInput;
            DialogueManager.OnDialogueStarted += _setPlayerControlsInactive;
            DialogueManager.OnDialogueEnded += _setPlayerControlsActive;
        }

        private void OnDisable()
        {
            SetInactiveControlPlayerInput(true, true);
            PauseManager.OnPauseTriggered -= SetInactiveControlPlayerInput;
            DialogueManager.OnDialogueStarted -= _setPlayerControlsInactive;
            DialogueManager.OnDialogueEnded -= _setPlayerControlsActive;
        }

        private void FixedUpdate()
        {
            rb.velocity = _playerMovement * movementSpeed;
        }

        private void OnMovement(InputValue ctx)
        {
            _playerMovement = ctx.Get<Vector2>();
        }

        private void OnInventoryChange(InputValue ctx)
        {
            int val = (int)ctx.Get<float>();
            if (val < 1) return;

            OnInventoryFocusSlotChanged?.Invoke(val - 1);
        }

        private void OnAim(InputValue ctx)
        {
            _mousePosition = ctx.Get<Vector2>();
            OnAimed?.Invoke(_mousePosition);
        }

        private void OnInteract(InputValue ctx)
        {
            int val = (int)ctx.Get<float>();
            if (val < 1) return;

            OnInteracted?.Invoke(_mousePosition);
        }

        private void SetInactiveControlPlayerInput(bool isInactive, bool _)
        {
            if (isInactive) _playerInput.Disable();
            else _playerInput.Enable();
        }
    }
}

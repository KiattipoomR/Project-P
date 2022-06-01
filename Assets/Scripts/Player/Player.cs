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
        [SerializeField] private Animator animator;

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

        private string _currentAnimationState;

        private const string PLAYER_IDLE_UP = "BodyIdleUp";
        private const string PLAYER_IDLE_DOWN = "BodyIdleDown";
        private const string PLAYER_IDLE_RIGHT = "BodyIdleRight";
        private const string PLAYER_IDLE_LEFT = "BodyIdleLeft";
        private const string PLAYER_WALK_UP = "BodyWalkUp";
        private const string PLAYER_WALK_DOWN = "BodyWalkDown";
        private const string PLAYER_WALK_RIGHT = "BodyWalkRight";
        private const string PLAYER_WALK_LEFT = "BodyWalkLeft";

        protected override void Awake()
        {
            base.Awake();
            _playerInput = GetComponent<PlayerInput>().actions;
            _setPlayerControlsInactive = () => SetInactiveControlPlayerInput(true, "");
            _setPlayerControlsActive = () => SetInactiveControlPlayerInput(false, "");
        }

        private void OnEnable()
        {
            SetInactiveControlPlayerInput(false, "");
            PauseManager.OnPauseTriggered += SetInactiveControlPlayerInput;
            DialogueManager.OnDialogueStarted += _setPlayerControlsInactive;
            DialogueManager.OnDialogueEnded += _setPlayerControlsActive;
        }

        private void OnDisable()
        {
            SetInactiveControlPlayerInput(true, "");
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

            if (_playerMovement.x > 0)
        {
            ChangeAnimationState(PLAYER_WALK_RIGHT);
        }
        else if (_playerMovement.x < 0)
        {
            ChangeAnimationState(PLAYER_WALK_LEFT);
        }
        else if (_playerMovement.y > 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP);
        }
        else if (_playerMovement.y < 0)
        {
            ChangeAnimationState(PLAYER_WALK_DOWN);
        }
        else
        {
            string newAnimationState;
            switch (_currentAnimationState)
            {
                case PLAYER_WALK_UP:
                    newAnimationState = PLAYER_IDLE_UP;
                    break;
                case PLAYER_WALK_DOWN:
                    newAnimationState = PLAYER_IDLE_DOWN;
                    break;
                case PLAYER_WALK_RIGHT:
                    newAnimationState = PLAYER_IDLE_RIGHT;
                    break;
                case PLAYER_WALK_LEFT:
                    newAnimationState = PLAYER_IDLE_LEFT;
                    break;
                default:
                    newAnimationState = PLAYER_IDLE_DOWN;
                    break;
            }

            ChangeAnimationState(newAnimationState);
        }
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

        private void SetInactiveControlPlayerInput(bool isInactive, string _)
        {
            if (isInactive) _playerInput.Disable();
            else _playerInput.Enable();
        }

        private void ChangeAnimationState(string newAnimationState)
        {
            // Prevent animation from interrupting itself
            if (_currentAnimationState == newAnimationState) return;

            // Play new animation
            animator.Play(newAnimationState);

            // Update current state
            
            _currentAnimationState = newAnimationState;
        }
    }
}

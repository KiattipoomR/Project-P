using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Manager
{
    public class PauseManager : MonoBehaviour
    {
        public static UnityAction<bool> OnPauseTriggered;

        private InputActionAsset _playerInput;
        private bool _isPaused = false;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>().actions;
        }

        private void OnEnable()
        {
            SetInactiveControlPlayerInput(false);
        }

        private void OnDisable()
        {
            SetInactiveControlPlayerInput(true);
        }

        private void OnPause()
        {
            _isPaused = !_isPaused;

            Time.timeScale = _isPaused ? 0 : 1;

            OnPauseTriggered?.Invoke(_isPaused);
        }

        private void SetInactiveControlPlayerInput(bool isInactive)
        {
            if (isInactive) _playerInput.Disable();
            else _playerInput.Enable();
        }
    }
}
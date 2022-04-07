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
            ControlPlayerInput(false);
        }

        private void OnDisable()
        {
            ControlPlayerInput(true);
        }

        private void OnPause()
        {
            _isPaused = !_isPaused;

            Time.timeScale = _isPaused ? 0 : 1;

            OnPauseTriggered?.Invoke(_isPaused);
        }

        private void ControlPlayerInput(bool isPaused)
        {
            if (isPaused) _playerInput.Disable();
            else _playerInput.Enable();
        }
    }
}
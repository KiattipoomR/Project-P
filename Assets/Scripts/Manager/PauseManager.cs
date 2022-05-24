using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Manager
{
    public class PauseManager : MonoBehaviour
    {
        public static UnityAction<bool, string> OnPauseTriggered;

        private InputActionAsset _playerInput;
        private bool _isPaused;

        private UnityAction _setPlayerControlsInactive;
        private UnityAction _setPlayerControlsActive;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>().actions;
            _setPlayerControlsInactive = () => SetInactiveControlPlayerInput(true);
            _setPlayerControlsActive = () => SetInactiveControlPlayerInput(false);
        }

        private void OnEnable()
        {
            SetInactiveControlPlayerInput(false);
            DialogueManager.OnDialogueStarted += _setPlayerControlsInactive;
            DialogueManager.OnDialogueStarted += SystemPause;
            DialogueManager.OnDialogueEnded += _setPlayerControlsActive;
            DialogueManager.OnDialogueEnded += SystemPause;
            WorkerManager.OnToggleTriggered += SetInactiveControlPlayerInput;
            WorkerManager.OnToggleTriggered += PlayerPause;
        }

        private void OnDisable()
        {
            SetInactiveControlPlayerInput(true);
            DialogueManager.OnDialogueStarted -= _setPlayerControlsInactive;
            DialogueManager.OnDialogueStarted -= SystemPause;
            DialogueManager.OnDialogueEnded -= _setPlayerControlsActive;
            DialogueManager.OnDialogueEnded -= SystemPause;
            WorkerManager.OnToggleTriggered -= SetInactiveControlPlayerInput;
            WorkerManager.OnToggleTriggered -= PlayerPause;

        }

        private void OnPause()
        {
            TriggerPause();

            OnPauseTriggered?.Invoke(_isPaused, "Player");
        }

        private void SystemPause()
        {
            TriggerPause();

            OnPauseTriggered?.Invoke(_isPaused, "Dialogue");
        }

        private void PlayerPause(bool _)
        {
            TriggerPause();

            OnPauseTriggered?.Invoke(_isPaused, "Worker");
        }

        private void TriggerPause()
        {
            _isPaused = !_isPaused;

            Time.timeScale = _isPaused ? 0 : 1;
        }

        private void SetInactiveControlPlayerInput(bool isInactive)
        {
            if (isInactive) _playerInput.Disable();
            else _playerInput.Enable();
        }
    }
}
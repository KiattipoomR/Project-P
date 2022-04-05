using Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Manager
{
    public class PauseManager : SingletonMonobehaviour<PauseManager>
    {
        public UnityAction<bool> OnPauseTriggered;

        private bool _isPaused = false;
        
        private void OnPause()
        {
            _isPaused = !_isPaused;
            
            if(_isPaused) PauseGame();
            else UnpauseGame();
            
            OnPauseTriggered?.Invoke(_isPaused);
        }

        private static void PauseGame()
        {
            Time.timeScale = 0;
            Player.Player.Instance.GetComponent<PlayerInput>().actions.Disable();
        }

        private static void UnpauseGame()
        {
            Time.timeScale = 1;
            Player.Player.Instance.GetComponent<PlayerInput>().actions.Enable();
        }
    }
}
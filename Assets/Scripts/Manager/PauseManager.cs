using Misc;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class PauseManager : SingletonMonobehaviour<PauseManager>
    {
        public UnityAction<bool> OnPauseTriggered;

        private bool _isPaused = false;

        private void OnPause()
        {
            _isPaused = !_isPaused;

            Time.timeScale = _isPaused ? 0 : 1;

            OnPauseTriggered?.Invoke(_isPaused);
        }
    }
}
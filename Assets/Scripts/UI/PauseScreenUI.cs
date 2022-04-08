using System;
using Manager;
using UnityEngine;

namespace UI
{
    public class PauseScreenUI : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private RectTransform[] _hideableObjects;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _hideableObjects = Array.FindAll(GetComponentsInChildren<RectTransform>(), hideableObject => hideableObject != _rectTransform);

            SetActiveInventoryBar(false);
        }
        
        private void OnEnable()
        {
            PauseManager.OnPauseTriggered += SetActiveInventoryBar;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= SetActiveInventoryBar;
        }

        private void SetActiveInventoryBar(bool isActive)
        {
            foreach (RectTransform hideableObject in _hideableObjects)
            {
                hideableObject.gameObject.SetActive(isActive);
            }
        }
    }
}
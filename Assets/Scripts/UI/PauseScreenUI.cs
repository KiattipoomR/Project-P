using Manager;
using UnityEngine;

namespace UI
{
    public class PauseScreenUI : MonoBehaviour
    {
        [SerializeField] private RectTransform[] hideableObjects;

        private void Awake()
        {
            SetActiveInventoryBar(false, true);
        }

        private void OnEnable()
        {
            PauseManager.OnPauseTriggered += SetActiveInventoryBar;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= SetActiveInventoryBar;
        }

        private void SetActiveInventoryBar(bool isActive, bool fromPlayer)
        {
            if (!fromPlayer) return;
            
            foreach (RectTransform hideableObject in hideableObjects)
            {
                hideableObject.gameObject.SetActive(isActive);
            }
        }
    }
}
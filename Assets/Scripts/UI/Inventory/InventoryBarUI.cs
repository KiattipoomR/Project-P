using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Inventory
{
    public class InventoryBarUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InventoryBarSlotUI[] barSlots;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                _rectTransform.pivot = new Vector2(0.5f, 0f);
                _rectTransform.anchorMin = new Vector2(0.5f, 0f);
                _rectTransform.anchorMax = new Vector2(0.5f, 0f);
                _rectTransform.anchoredPosition = new Vector2(0f, 2.5f);
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                _rectTransform.pivot = new Vector2(0.5f, 1f);
                _rectTransform.anchorMin = new Vector2(0.5f, 1f);
                _rectTransform.anchorMax = new Vector2(0.5f, 1f);
                _rectTransform.anchoredPosition = new Vector2(0f, -2.5f);
            }
        }

        private void OnEnable()
        {
            PauseManager.OnPauseTriggered += SetActiveInventoryBar;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= SetActiveInventoryBar;
        }

        private void SetActiveInventoryBar(bool isPaused)
        {
            foreach (InventoryBarSlotUI barSlot in barSlots)
            {
                barSlot.gameObject.SetActive(!isPaused);
            }
        }
    }
}
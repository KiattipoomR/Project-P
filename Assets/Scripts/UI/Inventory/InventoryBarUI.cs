using System;
using System.Collections.Generic;
using Inventory;
using Manager;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Inventory
{
    public class InventoryBarUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InventoryBarSlotUI[] barSlots;
        [SerializeField] private PlayerInventoryHolder playerInventoryHolder;

        private Dictionary<InventoryBarSlotUI, ItemStack> _slotDictionary;

        private RectTransform _rectTransform;
        private RectTransform[] _hideableObjects;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _hideableObjects = Array.FindAll(GetComponentsInChildren<RectTransform>(), hideableObject => hideableObject != _rectTransform);
        }

        private void Start()
        {
            if (!playerInventoryHolder)
            {
                Debug.LogWarning($"No inventory assigned to {this.gameObject}");
                return;
            }

            playerInventoryHolder.OnFocusSlotChanged += UpdateFocusSlot;
            playerInventoryHolder.Inventory.OnInventorySlotUpdated += UpdateSlot;

            AssignSlot();
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
            PauseManager.OnPauseTriggered += SetInactiveInventoryBar;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= SetInactiveInventoryBar;
        }

        private void SetInactiveInventoryBar(bool isInactive)
        {
            foreach (RectTransform hideableObject in _hideableObjects)
            {
                hideableObject.gameObject.SetActive(!isInactive);
            }
        }

        private void AssignSlot()
        {
            _slotDictionary = new Dictionary<InventoryBarSlotUI, ItemStack>();

            for (var i = 0; i < 9; i++)
            {
                _slotDictionary.Add(barSlots[i], playerInventoryHolder.Inventory.Slots[i]);
                barSlots[i].Init(playerInventoryHolder.Inventory.Slots[i]);
            }
        }

        private void UpdateSlot(ItemStack updatedSlot)
        {
            foreach (var (key, value) in _slotDictionary)
            {
                if (value == updatedSlot)
                {
                    key.UpdateUISlot(updatedSlot);
                }
            }
        }

        private void UpdateFocusSlot(int slotIndex)
        {
            for (int i = 0; i < 9; i++)
            {
                barSlots[i].SetFocus(i == slotIndex);
            }
        }
    }
}
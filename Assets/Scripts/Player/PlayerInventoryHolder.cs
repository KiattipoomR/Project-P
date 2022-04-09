using Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInventoryHolder : InventoryHolder
    {
        [SerializeField] private int focusSlot;

        public UnityAction<int> OnFocusSlotChanged;

        private void Start()
        {
            SetFocusSlot(0);
        }

        private void OnEnable()
        {
            Player.OnInventoryFocusSlotChanged += SetFocusSlot;
        }

        private void OnDisable()
        {
            Player.OnInventoryFocusSlotChanged -= SetFocusSlot;
        }

        private void SetFocusSlot(int slotIndex)
        {
            focusSlot = slotIndex;
            OnFocusSlotChanged?.Invoke(focusSlot);
        }
    }
}
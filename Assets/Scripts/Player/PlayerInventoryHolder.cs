using Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInventoryHolder : InventoryHolder
    {
        [SerializeField] private int focusSlot;

        public UnityAction<int> OnFocusSlotChanged;
        public UnityAction<ItemStack> OnItemFocused;
        public UnityAction<ItemStack> OnInteracted;

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
            OnItemFocused?.Invoke(GetCurrentSlot());
        }

        private ItemStack GetCurrentSlot()
        {
            return Inventory.Slots[focusSlot];
        }
    }
}
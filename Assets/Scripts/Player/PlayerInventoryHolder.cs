using Inventory;
using Item;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInventoryHolder : InventoryHolder
    {
        [SerializeField] private int focusSlot;

        public UnityAction<int> OnFocusSlotChanged;
        public UnityAction<ItemStack> OnItemFocused;
        public UnityAction<Vector3, ItemStack> OnItemUsed;

        private void Start()
        {
            SetFocusSlot(0);
        }

        private void OnEnable()
        {
            Player.OnInventoryFocusSlotChanged += SetFocusSlot;
            Player.OnInteracted += UseItem;
        }

        private void OnDisable()
        {
            Player.OnInventoryFocusSlotChanged -= SetFocusSlot;
            Player.OnInteracted -= UseItem;
        }

        public bool AddItemToInventory(ItemData item, int amount)
        {
            bool addSuccess = Inventory.AddToInventory(item, amount);
            if (addSuccess && item == GetCurrentSlot().ItemData)  OnItemFocused?.Invoke(GetCurrentSlot());
            
            return addSuccess;
        }

        public void RemoveItemFromInventory(ItemData item, int amount)
        {
            Inventory.RemoveFromInventory(item, amount);
            OnItemFocused?.Invoke(GetCurrentSlot());
        }

        private void SetFocusSlot(int slotIndex)
        {
            focusSlot = slotIndex;
            OnFocusSlotChanged?.Invoke(focusSlot);
            OnItemFocused?.Invoke(GetCurrentSlot());
        }

        private void UseItem(Vector3 mousePosition)
        {
            OnItemUsed?.Invoke(mousePosition, GetCurrentSlot());
        }

        private ItemStack GetCurrentSlot()
        {
            return Inventory.Slots[focusSlot];
        }
    }
}
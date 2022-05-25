using System.Collections.Generic;
using Inventory;
using Player;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryPageUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InventoryPageSlotUI[] slots;
        [SerializeField] private PlayerInventoryHolder playerInventoryHolder;
        [SerializeField] private MouseItem mouseItem;

        private Dictionary<InventoryPageSlotUI, ItemStack> _slotDictionary;

        private void Start()
        {
            if (!playerInventoryHolder)
            {
                Debug.LogWarning($"No inventory assigned to {this.gameObject}");
                return;
            }

            playerInventoryHolder.Inventory.OnInventorySlotUpdated += UpdateSlot;

            AssignSlot();
        }

        private void AssignSlot()
        {
            _slotDictionary = new Dictionary<InventoryPageSlotUI, ItemStack>();

            for (var i = 0; i < 36; i++)
            {
                _slotDictionary.Add(slots[i], playerInventoryHolder.Inventory.Slots[i]);
                slots[i].Init(playerInventoryHolder.Inventory.Slots[i]);
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

        public void OnSlotClicked(InventoryPageSlotUI slot)
        {
            if (mouseItem.AssignedItemSlot.ItemData == null && slot.AssignedItemSlot.ItemData != null)
            {
                mouseItem.UpdateMouseSlot(slot.AssignedItemSlot, slot);
                slot.ClearSlot();
            }
            else if (mouseItem.AssignedItemSlot.ItemData != null && slot.AssignedItemSlot.ItemData == null)
            {
                slot.AssignedItemSlot.AssignItem(mouseItem.AssignedItemSlot);
                slot.UpdateUISlot();

                mouseItem.ClearSlot();
            }
            else if (mouseItem.AssignedItemSlot.ItemData != null && slot.AssignedItemSlot.ItemData != null)
            {
                // if (mouseItem.AssignedItemSlot.ItemData == slot.AssignedItemSlot.ItemData)
                // {
                //     return;
                // }
                // else
                // {
                ItemStack tempItemSlot = new ItemStack(slot.AssignedItemSlot.ItemData, slot.AssignedItemSlot.Stack);
                slot.AssignedItemSlot.AssignItem(mouseItem.AssignedItemSlot);
                slot.UpdateUISlot();

                mouseItem.UpdateMouseSlot(tempItemSlot, null);
                //     return;
                // }
            }
        }

        private void OnEnable()
        {
            if (_slotDictionary == null) return;
            for (var i = 0; i < 36; i++)
            {
                bool found = _slotDictionary.TryGetValue(slots[i], out ItemStack invSlot);
                if (found) slots[i].UpdateUISlot(invSlot);
            }
        }
    }
}
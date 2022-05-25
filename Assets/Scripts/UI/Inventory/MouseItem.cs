using System;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class MouseItem : MonoBehaviour
    {
        [Header("Base Components")]
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TextMeshProUGUI itemCount;
        [SerializeField] private ItemStack assignedSlot;

        private InventoryPageSlotUI _inventorySlot;

        public ItemStack AssignedItemSlot => assignedSlot;

        private void Awake()
        {
            ClearSlot();
        }

        private void OnDisable()
        {
            if (!_inventorySlot) return;
            throw new NotImplementedException();
        }

        private void Update()
        {
            if (assignedSlot.ItemData == null) return;
            transform.position = Mouse.current.position.ReadValue();
            
        }

        public void UpdateMouseSlot(ItemStack stack, InventoryPageSlotUI reference)
        {
            AssignedItemSlot.AssignItem(stack);
            itemImage.sprite = stack.ItemData.ItemIcon;
            itemCount.text = stack.Stack > 1 ? stack.Stack.ToString() : "";
            itemImage.color = Color.white;
            _inventorySlot = reference;
        }

        public void ClearSlot()
        {
            AssignedItemSlot?.ClearStack();
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemCount.text = "";
            _inventorySlot = null;
        }
    }
}
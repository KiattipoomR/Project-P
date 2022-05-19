using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        [Header("Base Components")]
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TextMeshProUGUI itemCount;
        [SerializeField] protected ItemStack assignedSlot;
        
        public ItemStack AssignedItemSlot => assignedSlot;
        
        private void Awake()
        {
            ClearSlot();
        }

        public void Init(ItemStack slot)
        {
            assignedSlot = slot;
            UpdateUISlot();
        }

        public void UpdateUISlot(ItemStack slot)
        {
            if (slot.ItemData)
            {
                itemImage.sprite = slot.ItemData.ItemIcon;
                itemImage.color = Color.white;

                itemCount.text = slot.Stack > 1 ? slot.Stack.ToString() : "";
            }
            else ClearSlot();
        }

        private void UpdateUISlot()
        {
            if (AssignedItemSlot != null) UpdateUISlot(AssignedItemSlot);
        }

        public void ClearSlot()
        {
            AssignedItemSlot?.ClearStack();
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemCount.text = "";
        }
    }
}
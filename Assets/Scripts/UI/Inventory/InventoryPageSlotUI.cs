using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryPageSlotUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemCount;
        [SerializeField] private ItemStack assignedSlot;
        
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
            if (assignedSlot != null) UpdateUISlot(assignedSlot);
        }

        public void ClearSlot()
        {
            assignedSlot?.ClearStack();
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemCount.text = "";
        }
    }
}
using Item;
using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class ItemStack
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private int stack;

        public ItemData ItemData => itemData;
        public int Stack => stack;

        public ItemStack(ItemData item, int amount)
        {
            itemData = item;
            stack = amount;
        }

        public ItemStack()
        {
            ClearStack();
        }

        public void ClearStack()
        {
            itemData = null;
            stack = -1;
        }

        public void UpdateStack(ItemData data, int amount)
        {
            itemData = data;
            stack = amount;
        }

        private bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)
        {
            amountRemaining = itemData.MaxStack - amountToAdd;
            return EnoughRoomLeftInStack(amountToAdd);
        }

        public bool EnoughRoomLeftInStack(int amountToAdd)
        {
            return stack + amountToAdd <= itemData.MaxStack;
        }

        public void AddToStack(int amount)
        {
            stack += amount;
        }

        public void RemoveFromStack(int amount)
        {
            stack -= amount;
        }
    }
}
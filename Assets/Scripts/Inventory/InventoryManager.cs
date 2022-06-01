using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
  [System.Serializable]
  public class InventoryManager
  {
    [SerializeField] private List<ItemStack> slots;

    public UnityAction<ItemStack> OnInventorySlotUpdated;

    public List<ItemStack> Slots => slots;
    public int InventorySize => slots.Count;

    public InventoryManager(int inventorySize)
    {
      slots = new List<ItemStack>();
      for (int i = 0; i < inventorySize; i++)
      {
        slots.Add(new ItemStack());
      }
    }

    public bool AddToInventory(ItemData item, int amount)
    {
      if (ContainsItem(item, out var availableSlots))
      {
        ItemStack slot = availableSlots.FirstOrDefault(invSlot => invSlot.EnoughRoomLeftInStack(amount));
        if (slot != null)
        {
          slot.AddToStack(amount);

          OnInventorySlotUpdated?.Invoke(slot);
          return true;
        }
      }

      if (!HasFreeSlot(out var freeSlot)) return false;

      freeSlot.UpdateStack(item, amount);

      OnInventorySlotUpdated?.Invoke(freeSlot);
      return true;
    }

    public bool RemoveFromInventory(ItemData item, int amount)
    {
      List<ItemStack> slotsContainingItem = slots.FindAll(invSlot => invSlot.ItemData == item);
      int totalAmountInInventory = slotsContainingItem.Sum(slot => slot.Stack);
      Debug.Log(totalAmountInInventory);
      if (totalAmountInInventory < amount) return false;

      foreach (ItemStack slot in slotsContainingItem)
      {
        if (slot.Stack > amount)
        {
          slot.RemoveFromStack(amount);
          OnInventorySlotUpdated?.Invoke(slot);
          break;
        }

        amount -= slot.Stack;
        slot.ClearStack();
        OnInventorySlotUpdated?.Invoke(slot);

        if (amount < 1) break;
      }

      return true;
    }

    public bool ContainsItem(ItemData item, out List<ItemStack> invSlots)
    {
      invSlots = Slots.Where(slot => slot.ItemData == item).ToList();
      return invSlots.Count > 0;
    }

    public bool HasFreeSlot(out ItemStack freeSlot)
    {
      freeSlot = Slots.FirstOrDefault(slot => slot.ItemData == null);
      return freeSlot != null;
    }
  }
}
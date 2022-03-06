using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryManager
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventoryManager(int size)
    {
        inventorySlots = new List<InventorySlot>();
        for (var i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(ItemData item, int amount)
    {
        if (ContainsItem(item, out var invSlots))
        {
            foreach (var slot in invSlots)
            {
                if (!slot.EnoughRoomLeftInStack(amount)) continue;

                slot.AddToStack(amount);
                OnInventorySlotChanged?.Invoke(slot);
                return true;
            }

        }

        if (HasFreeSlot(out var freeSlot))
        {
            freeSlot.UpdateInventorySlot(item, amount);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(ItemData item, out List<InventorySlot> invSlots)
    {
        invSlots = InventorySlots.Where(slot => slot.ItemData == item).ToList();
        return invSlots.Count > 0;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ItemData == null);
        return freeSlot != null;
    }
}
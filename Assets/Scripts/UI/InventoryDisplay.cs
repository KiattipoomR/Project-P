using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    protected InventoryManager inventoryManager;
    protected Dictionary<InventorySlotUI, InventorySlot> slotDictionary;

    public InventoryManager InventoryManager => inventoryManager;
    protected Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => slotDictionary;

    public abstract void AssignSlot(InventoryManager inventoryToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var (key, value) in SlotDictionary)
        {
            if (value == updatedSlot)
            {
                key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(InventorySlotUI clickedSlot)
    {

    }
}

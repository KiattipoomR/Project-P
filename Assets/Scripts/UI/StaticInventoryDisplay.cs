using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlotUI[] slots;


    protected void Start()
    {
        if (inventoryHolder)
        {
            inventoryManager = inventoryHolder.InventoryManager;
            inventoryManager.OnInventorySlotChanged += UpdateSlot;
        }
        else
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");


        inventoryManager = inventoryHolder.InventoryManager;
        inventoryManager.OnInventorySlotChanged += UpdateSlot;

        AssignSlot(inventoryManager);


        // ------------------------


        if (!inventoryHolder)
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
            return;
        }

        inventoryManager = inventoryHolder.InventoryManager;
        inventoryManager.OnInventorySlotChanged += UpdateSlot;

        AssignSlot(inventoryManager);
    }

    public override void AssignSlot(InventoryManager inventoryToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        if (slots.Length != inventoryManager.InventorySize)
            Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (var i = 0; i < inventoryManager.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventoryManager.InventorySlots[i]);
            slots[i].Init(inventoryManager.InventorySlots[i]);
        }
    }
}

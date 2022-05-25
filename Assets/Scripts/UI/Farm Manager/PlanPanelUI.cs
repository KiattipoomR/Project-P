using UnityEngine;
using TMPro;
using Manager;
using FarmManager;
using InventoryUI = UI.Inventory;
using Item;
using Inventory;

public class PlanPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI staminaNeedText;
    [SerializeField] private SeedSlotUI slot;
    [SerializeField] private InventoryUI.MouseItem mouseItem;

    private void OnEnable()
    {
        WorkerManager.OnRecalculatingStaminaNeeded += UpdateStaminaNeeded;
    }

    private void OnDisable()
    {
        WorkerManager.OnRecalculatingStaminaNeeded -= UpdateStaminaNeeded;
    }

    private void UpdateStaminaNeeded(int staminaNeeded)
    {
        staminaNeedText.text = string.Format("Stamina Needed: {0}", staminaNeeded);
    }

    public void OnSlotClicked()
    {
        if (mouseItem.AssignedItemSlot.ItemData == null)
        {
            if (slot.AssignedItemSlot.ItemData == null) return;

            SeedAndCropStack latestStack = GameManager.Instance.workerManager.RemoveSeedFromList();
            ItemStack tempItemSlot = new ItemStack(latestStack.SeedData, latestStack.Stack);
            slot.AssignedItemSlot.AssignItem(mouseItem.AssignedItemSlot);
            slot.UpdateUISlot();

            mouseItem.UpdateMouseSlot(tempItemSlot, null);
        }
        else
        {
            if (mouseItem.AssignedItemSlot.ItemData.ItemType != ItemType.Seed) return;

            GameManager.Instance.workerManager.AddSeedToList(new SeedAndCropStack((SeedData)mouseItem.AssignedItemSlot.ItemData, mouseItem.AssignedItemSlot.Stack));
            slot.AssignedItemSlot.UpdateStack(mouseItem.AssignedItemSlot.ItemData, mouseItem.AssignedItemSlot.Stack);
            slot.UpdateUISlot();

            mouseItem.ClearSlot();
        }
    }

    public void HarvestCrops()
    {
        GameManager.Instance.workerManager.HarvestCrops();
    }
}

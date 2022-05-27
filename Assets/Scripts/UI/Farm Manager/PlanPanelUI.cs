using UnityEngine;
using TMPro;
using Manager;
using FarmManager;
using InventoryUI = UI.Inventory;
using Item;
using Inventory;
using UnityEngine.UI;

public class PlanPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI staminaNeedText;
    [SerializeField] private SeedSlotUI slot;
    [SerializeField] private InventoryUI.MouseItem mouseItem;

    [SerializeField] private TextMeshProUGUI cropsToBeCollectedText;
    [SerializeField] private Button collectCropsButton;

    private void OnEnable()
    {
        WorkerManager.OnRecalculatingStaminaNeeded += UpdateStaminaNeeded;
        UpdateStaminaNeeded(GameManager.Instance.workerManager.GetStaminaNeeded());
        
        int collectableCrops = GameManager.Instance.workerManager.GetCollectableCrops();
        SetEnableCollectCrops(collectableCrops > 0);
        if (collectableCrops > 0) SetCropsToBeCollectedText(collectableCrops);
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

    public void CollectCrops()
    {
        GameManager.Instance.workerManager.CollectCrops();
        SetEnableCollectCrops(false);
    }

    private void SetCropsToBeCollectedText(int cropsToBeCollected) {
        cropsToBeCollectedText.text = $"{cropsToBeCollected} crop(s) to be collected.";
    }

    private void SetEnableCollectCrops(bool isEnable) {
        cropsToBeCollectedText.gameObject.SetActive(isEnable);
        collectCropsButton.gameObject.SetActive(isEnable);
    }
}

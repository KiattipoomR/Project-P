using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using DateTime = GameTime.DateTime;

public class SeedSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private ItemStack assignedSlot;
    [SerializeField] private Button button;

    [SerializeField] private PlanPanelUI reference;

    public ItemStack AssignedItemSlot => assignedSlot;

    private void Awake()
    {
        TimeManager.OnDateTimeChanged += ResetSlotAfterDayChanged;
        ClearSlot();
        if (button != null) button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (reference != null) reference.OnSlotClicked();
    }

    private void UpdateUISlot(ItemStack slot)
    {
        if (slot.ItemData)
        {
            itemImage.sprite = slot.ItemData.ItemIcon;
            itemImage.color = Color.white;

            itemCount.text = slot.Stack > 1 ? slot.Stack.ToString() : "";
        }
        else ClearSlot();
    }

    public void UpdateUISlot()
    {
        if (AssignedItemSlot != null) UpdateUISlot(AssignedItemSlot);
    }

    private void ResetSlotAfterDayChanged(DateTime _)
    {
        ClearSlot();
    }

    private void ClearSlot()
    {
        AssignedItemSlot?.ClearStack();
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemCount.text = "";
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedItemSlot;
    [SerializeField] private Button button;

    public InventorySlot AssignedItemSlot => assignedItemSlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        if (button) button.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        assignedItemSlot = slot;
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData)
        {
            itemSprite.sprite = slot.ItemData.icon;
            itemSprite.color = Color.white;

            itemCount.text = slot.StackSize > 1 ? slot.StackSize.ToString() : "";
        }
        else ClearSlot();

    }

    void UpdateUISlot()
    {
        if (assignedItemSlot != null) UpdateUISlot(assignedItemSlot);
    }

    public void ClearSlot()
    {
        assignedItemSlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        if (ParentDisplay) ParentDisplay.SlotClicked(this);
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryPageSlotUI : InventorySlotUI
    {
        [Header("Additional Components For Inventory Page Slot")]
        [SerializeField] private Button button;
        [SerializeField] private InventoryPageUI reference;

        protected override void Awake()
        {
            base.Awake();
            if (button != null) button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (reference != null) reference.OnSlotClicked(this);
        }
    }
}
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryBarSlotUI : InventorySlotUI
    {
        [Header("Additional Components For Inventory Bar Slot")]
        [SerializeField] private Image focusOverlay;

        public void SetFocus(bool isFocused)
        {
            focusOverlay.gameObject.SetActive(isFocused);
        }
    }
}
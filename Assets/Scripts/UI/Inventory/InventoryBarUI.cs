using Manager;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryBarUI : MonoBehaviour
    {
        [SerializeField] private InventoryBarSlotUI[] barSlots;

        private void OnEnable()
        {
            PauseManager.OnPauseTriggered += SetActiveInventoryBar;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= SetActiveInventoryBar;
        }

        private void SetActiveInventoryBar(bool isPaused)
        {
            foreach (InventoryBarSlotUI barSlot in barSlots)
            {
                barSlot.gameObject.SetActive(!isPaused);
            }
        }
    }
}
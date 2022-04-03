using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{

    [SerializeField] private int backpackSize;
    [SerializeField] protected InventoryManager backpackManager;

    [SerializeField] private MovementManager movementManager;


    private int currentSlot;

    public InventoryManager BackpackManager => backpackManager;

    public UnityAction<int, int> OnCurrentHotbarSlotChanged;
    protected override void Awake()
    {
        base.Awake();
        currentSlot = 0;
        movementManager.OnCurrentHotbarChanged += ChangeCurrentHotbarSlot;

    }

    private void OnDestroy()
    {
        movementManager.OnCurrentHotbarChanged -= ChangeCurrentHotbarSlot;
    }

    public void ChangeCurrentHotbarSlot(int slot)
    {
        OnCurrentHotbarSlotChanged?.Invoke(currentSlot, slot);
        currentSlot = slot;
    }
}

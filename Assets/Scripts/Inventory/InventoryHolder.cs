using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventoryManager inventoryManager;

    public InventoryManager InventoryManager => inventoryManager;

    protected virtual void Awake()
    {
        inventoryManager = new InventoryManager(inventorySize);
    }
}
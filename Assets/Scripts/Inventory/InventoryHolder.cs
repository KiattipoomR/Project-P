using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventoryManager inventoryManager;

    public InventoryManager InventoryManager => inventoryManager;

    private void Awake()
    {
        inventoryManager = new InventoryManager(inventorySize);
    }
}
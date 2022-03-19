using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private ItemData itemData;
    [SerializeField] private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider.isTrigger = true;
        boxCollider.edgeRadius = radius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventoryHolder = other.transform.GetComponent<InventoryHolder>();
        if (!inventoryHolder) return;

        if (inventoryHolder.InventoryManager.AddToInventory(itemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
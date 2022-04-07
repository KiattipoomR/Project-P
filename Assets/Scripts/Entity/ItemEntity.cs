using System;
using Inventory;
using Item;
using UnityEngine;

namespace Entity
{
    public class ItemEntity : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ItemData item;

        private SpriteRenderer _renderer;
        private BoxCollider2D _collider;
        
        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();

            if (!item) Destroy(gameObject);
            _renderer.sprite = item.ItemIcon;
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            InventoryHolder inventoryHolder = other.GetComponent<InventoryHolder>();
            if (!inventoryHolder) return;

            if (!inventoryHolder.Inventory.AddToInventory(item, 1)) return;
            Destroy(gameObject);
        }
    }
}
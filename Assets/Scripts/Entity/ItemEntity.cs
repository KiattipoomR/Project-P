using Item;
using Player;
using UnityEngine;

namespace Entity
{
    public class ItemEntity : MonoBehaviour
    {
        [Header("Attributes")]
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
            PlayerInventoryHolder playerInventory = other.GetComponent<PlayerInventoryHolder>();
            if (!playerInventory) return;

            if (!playerInventory.AddItemToInventory(item, 1)) return;
            Destroy(gameObject);
        }
    }
}
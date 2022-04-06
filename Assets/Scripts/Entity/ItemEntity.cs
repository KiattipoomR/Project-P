using Item;
using UnityEngine;

namespace Entity
{
    public class ItemEntity : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ItemData item;

        private SpriteRenderer _renderer;
        
        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();

            if (!item) return;
            _renderer.sprite = item.itemIcon;
        }
    }
}
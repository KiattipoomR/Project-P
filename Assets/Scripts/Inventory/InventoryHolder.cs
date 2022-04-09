using System;
using UnityEngine;

namespace Inventory
{
    public class InventoryHolder : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryManager inventory;

        public InventoryManager Inventory => inventory;

        protected void Awake()
        {
            inventory = new InventoryManager(inventorySize);
        }
    }
}
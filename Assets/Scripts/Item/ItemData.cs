using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Item", fileName = "Item_ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("General Attributes")]
        [SerializeField] protected string itemID;
        [SerializeField] protected string itemName;
        [TextArea(1, 4)]
        [SerializeField] protected string itemDescription;
        [SerializeField] protected Sprite itemIcon;
        [SerializeField] protected ItemType itemType;
        [Range(1, 99)]
        [SerializeField] protected int maxStack;

        [Header("Price Attributes")]
        [SerializeField] protected int buyPrice;
        [SerializeField] protected int sellPrice;

        public string ItemID => itemID;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public Sprite ItemIcon => itemIcon;
        public virtual ItemType ItemType => itemType;
        public virtual int MaxStack => maxStack;
        public virtual int BuyPrice => buyPrice;
        public virtual int SellPrice => sellPrice;

        public bool IsSellable()
        {
            return SellPrice > 0;
        }

        public bool IsNonPlaceableItem()
        {
            return ItemType is ItemType.Misc or ItemType.Crop;
        }
    }

    [System.Serializable]
    public enum ItemType
    {
        Misc,
        Pickaxe,
        Axe,
        Scythe,
        Hoe,
        WateringCan,
        Crop,
        Seed,
    }
}
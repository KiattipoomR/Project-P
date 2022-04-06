using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Item", fileName = "Item_ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("General Attributes")]
        [SerializeField] private string itemID;
        [SerializeField] private string itemName;
        [TextArea(1, 4)]
        [SerializeField] private string itemDescription;
        [SerializeField] private Sprite itemIcon;

        [Header("Max Item Attributes")]
        [Range(1, 99)]
        [SerializeField] private int maxStack;

        [Header("Price Attributes")]
        [SerializeField] private int buyPrice;
        [SerializeField] private int sellPrice;

        public string ItemID => itemID;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public Sprite ItemIcon => itemIcon;
        public virtual int MaxStack => maxStack;
        public virtual int BuyPrice => buyPrice;
        public virtual int SellPrice => sellPrice;

        public bool IsSellable()
        {
            return SellPrice > 0;
        }
    }
}
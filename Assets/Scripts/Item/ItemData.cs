using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Item", fileName = "Item_ItemData")]
    public class ItemData : ScriptableObject
    {
        public int itemID;
        public string itemName;
        public string itemDescription;
        public Sprite itemIcon;
    }
}
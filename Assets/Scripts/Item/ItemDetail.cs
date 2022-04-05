using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Item", fileName = "Item_ItemData")]
    public class ItemDetail : ScriptableObject
    {
        public int itemID;
        public string itemDescription;
        public Sprite itemIcon;
    }
}
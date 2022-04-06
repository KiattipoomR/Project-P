using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Manager
{
    public class DataManager : MonoBehaviour
    {
        private readonly Dictionary<string, ItemData> _itemDataDictionary = new();

        private void Awake()
        {
            ItemData[] itemData = Resources.LoadAll<ItemData>("ScriptableObjects");

            foreach (ItemData item in itemData)
            {
                if (item.ItemID == "") continue;
                _itemDataDictionary.Add(item.ItemID, item);
            }
        }

        public ItemData GetItemDataByItemID(string itemID)
        {
            return _itemDataDictionary.TryGetValue(itemID, out ItemData item) ? item : null;
        }
    }
}
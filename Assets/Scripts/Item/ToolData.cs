using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Tool", fileName = "Item_Tool_ToolData")]
    public class ToolData : ItemData
    {
        public override int MaxStack => 1;
        public override int BuyPrice => 0;
        public override int SellPrice => 0;
    }
}
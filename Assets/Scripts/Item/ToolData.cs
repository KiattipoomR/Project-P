using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Tool", fileName = "Item_Tool_ToolData")]
    public class ToolData : ItemData
    {
        [Header("Tool Attributes")]
        [SerializeField] private ToolType toolType;
        [SerializeField] private int staminaCost;
        
        
        public override int MaxStack => 1;
        public override int BuyPrice => 0;
        public override int SellPrice => 0;
        
        public ToolType ToolType => toolType;
        public int StaminaCost => staminaCost;
    }

    [System.Serializable]
    public enum ToolType
    {
        Pickaxe,
        Axe,
        Reaper,
        Hoe,
        WateringCan,
    }
}
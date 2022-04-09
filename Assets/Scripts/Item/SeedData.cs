using Crop;
using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Seed", fileName = "Item_Seed_SeedData")]
    public class SeedData : ItemData
    {
        [Header("Crop Attributes")]
        [SerializeField] private CropData cropData;

        public override ItemType ItemType => ItemType.Seed;

        public CropData CropData => cropData;
    }
}
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Crop
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptable Object/Crop", fileName = "Crop_CropData")]
    public class CropData : ScriptableObject
    {
        [SerializeField] private string id;
        
        [SerializeField] private List<CropStage> stages;
        [SerializeField] private ItemData harvestedItem;

        public string ID => id;
        public List<CropStage> Stages => stages;
        public ItemData HarvestedItem => harvestedItem;
    }

    [System.Serializable]
    public struct CropStage
    {
        [SerializeField] private int growthDays;
        [SerializeField] private Sprite growthStageSprite;

        public int GrowthDays => growthDays;
        public Sprite GrowthStageSprite => growthStageSprite;
    }
}
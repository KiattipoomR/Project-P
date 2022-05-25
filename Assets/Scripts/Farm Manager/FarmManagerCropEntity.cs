using Crop;
using Manager;
using UnityEngine;
using Item;

namespace FarmManager
{
  public class FarmManagerCropEntity
  {
    [SerializeField] private CropData cropData;

    private int _currentCropStage;
    private int _growthDays;

    public FarmManagerCropEntity(CropData crop)
    {
      cropData = crop;
      _currentCropStage = 0;
      _growthDays = 0;
    }

    public void Grow()
    {
      if (_currentCropStage == cropData.Stages.Count - 1) return;

      _growthDays++;
      if (_growthDays < cropData.Stages[_currentCropStage].GrowthDays) return;

      _growthDays = 0;
      _currentCropStage++;
    }

    public bool IsHarvestable => _currentCropStage == cropData.Stages.Count - 1;
    public int StaminaNeededForPlantDay => cropData.Stages[_currentCropStage].StaminaPerDay;
    public int StaminaNeededForHarvestDay => cropData.StaminaPerHarvest;
    public ItemData HarvestProduct => cropData.HarvestedItem;
  }
}
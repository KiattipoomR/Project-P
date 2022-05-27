using UnityEngine;
using Item;
using Inventory;

namespace FarmManager
{
    public enum PlantingStatus
  {
    SEED,
    PLANT,
    HARVEST,
    DEAD
  }

  public class SeedAndCropStack
  {
    [SerializeField] SeedData seedData;
    [SerializeField] FarmManagerCropEntity cropEntity;
    [SerializeField] int stack;

    PlantingStatus status;

    public SeedData SeedData => seedData;
    public int Stack => stack;
    public PlantingStatus Status => status;

    public SeedAndCropStack(SeedData seedData, int stack)
    {
      this.seedData = seedData;
      this.cropEntity = null;
      this.stack = stack;
      this.status = PlantingStatus.SEED;
    }

    public ItemStack ProceedNewDay()
    {
      if (status == PlantingStatus.SEED)
      {
        cropEntity = new FarmManagerCropEntity(seedData.CropData);
        status = PlantingStatus.PLANT;
      }
      else if (status == PlantingStatus.PLANT)
      {
        cropEntity.Grow();
        if (cropEntity.IsHarvestable)
        {
          status = PlantingStatus.HARVEST;
        }
      }
      else if (status == PlantingStatus.HARVEST)
      {
        status = PlantingStatus.DEAD;
        return new ItemStack(cropEntity.HarvestProduct, stack);
      }
      return null;
    }

    public int GetStaminaNeeded()
    {
      if (status == PlantingStatus.SEED)
      {
        return seedData.StaminaPerSeedPlanting * stack;
      }
      else if (status == PlantingStatus.PLANT)
      {
        return cropEntity.StaminaNeededForPlantDay * stack;
      }
      else if (status == PlantingStatus.HARVEST)
      {
        return cropEntity.StaminaNeededForHarvestDay * stack;
      }
      return 0;
    }
  }
}
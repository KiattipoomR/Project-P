using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Manager;
using Item;
using FarmManager;

public class PlanPanelUI : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI staminaNeedText;
  [SerializeField] SeedData testSeed;

  private void OnEnable()
  {
    WorkerManager.OnRecalculatingStaminaNeeded += UpdateStaminaNeeded;
  }

  private void OnDisable()
  {
    WorkerManager.OnRecalculatingStaminaNeeded -= UpdateStaminaNeeded;
  }

  private void UpdateStaminaNeeded(int staminaNeeded)
  {
    staminaNeedText.text = string.Format("Stamina Needed: {0}", staminaNeeded);
  }

  public void AddSeedToQueue()
  {
    GameManager.Instance.workerManager.AddSeedToList(new SeedAndCropStack(testSeed, 10));
  }
}

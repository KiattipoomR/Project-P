using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using FarmManager;
using Inventory;

namespace Manager
{
  public class WorkerManager : MonoBehaviour
  {
    public static UnityAction<bool> OnToggleTriggered;
    public static UnityAction<List<Worker>> OnRecruitableWorkerListChanged;
    public static UnityAction<List<Worker>> OnRecruitedWorkerListChanged;
    public static UnityAction<int> OnRecalculatingStaminaNeeded;
    private InputActionAsset _playerInput;

    [SerializeField] private int recruitedWorkerLimit = 5;
    [SerializeField] private int recruitableWorkerLimit = 5;
    private WorkerList recruitedWorkerList;
    private WorkerList recruitableWorkerList;

    private List<SeedAndCropStack> seedListForNextDay;
    private List<SeedAndCropStack> seedAndCropListInProcess;
    private List<ItemStack> harvestedCropList;

    private bool _isToggled = false;

    public WorkerList RecruitedWorkerList => recruitedWorkerList;
    public WorkerList RecruitableWorkerList => recruitableWorkerList;

    private void Awake()
    {
      _playerInput = GetComponent<PlayerInput>().actions;
    }

    private void Start()
    {
      recruitedWorkerList = new WorkerList(recruitedWorkerLimit);
      recruitableWorkerList = new WorkerList(recruitableWorkerLimit);
      OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
      RenewRecruitableWorkerList();
      seedListForNextDay = new List<SeedAndCropStack>();
      seedAndCropListInProcess = new List<SeedAndCropStack>();
      harvestedCropList = new List<ItemStack>();
    }

    private void OnEnable()
    {
      SetInactiveControlPlayerInput(false);
      TimeManager.OnDayChanged += RenewRecruitableWorkerList;
      TimeManager.OnDayChanged += ExecuteFarmPlan;
    }

    private void OnDisable()
    {
      SetInactiveControlPlayerInput(true);
      TimeManager.OnDayChanged -= RenewRecruitableWorkerList;
      TimeManager.OnDayChanged -= ExecuteFarmPlan;
    }

    private void OnToggle()
    {
      _isToggled = !_isToggled;
      OnToggleTriggered?.Invoke(_isToggled);
      //RefreshWorkerList();
    }

    private void RenewRecruitableWorkerList()
    {
      recruitableWorkerList.ClearWorkerList();
      recruitableWorkerList.GenerateWorkerListToFull();
      OnRecruitableWorkerListChanged?.Invoke(recruitableWorkerList.Workers);
    }

    private void RefreshWorkerList()
    {
      OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
      OnRecruitableWorkerListChanged?.Invoke(recruitableWorkerList.Workers);
    }

    public bool RecruitWorkerFromRecruitableList(string workerId)
    {
      Worker worker = recruitableWorkerList.GetWorkerById(workerId);
      if (worker == null)
      {
        return false;
      }
      bool res = recruitedWorkerList.AddWorker(worker);
      if (res)
      {
        recruitableWorkerList.RemoveWorker(workerId);
        OnRecruitableWorkerListChanged?.Invoke(recruitableWorkerList.Workers);
        OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
        return true;
      }
      return false;
    }

    public bool FireWorker(string workerId)
    {
      bool res = recruitedWorkerList.RemoveWorker(workerId);
      if (res)
      {
        OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
        return true;
      }
      return false;
    }

    private void SetInactiveControlPlayerInput(bool isInactive)
    {
      if (isInactive) _playerInput.Disable();
      else _playerInput.Enable();
    }

    private int staminaNeededInProcessTomorrow = 0;

    private void ProjectedStaminaNeededForTomorrowWithPlan()
    {
      int staminaNeeded = staminaNeededInProcessTomorrow;
      foreach (SeedAndCropStack i in seedListForNextDay)
      {
        staminaNeeded += i.GetStaminaNeeded();
      }
      OnRecalculatingStaminaNeeded?.Invoke(staminaNeeded);
    }

    public void AddSeedToList(SeedAndCropStack seedStack)
    {
      seedListForNextDay.Add(seedStack);
      ProjectedStaminaNeededForTomorrowWithPlan();
    }

    private void ReduceTodayWorkerStamina(int staminaUsed)
    {
      recruitedWorkerList.Work(staminaUsed);
      OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
    }

    public void SetRecruitedWorkerIsActive(string workerId, bool newIsActive)
    {
      if (recruitedWorkerList.SetWorkerIsActive(workerId, newIsActive))
      {
        OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
      }
    }

    private void ExecuteFarmPlan()
    {
      int staminaUsedInProcessToday = 0;
      staminaNeededInProcessTomorrow = 0;
      foreach (SeedAndCropStack i in seedAndCropListInProcess)
      {
        staminaUsedInProcessToday += i.GetStaminaNeeded();
        ItemStack product = i.ProceedNewDay();
        if (product != null)
        {
          harvestedCropList.Add(product);
        }
        staminaNeededInProcessTomorrow += i.GetStaminaNeeded();
      }
      seedAndCropListInProcess.RemoveAll(s => s.Status == PlantingStatus.DEAD);
      foreach (SeedAndCropStack i in seedListForNextDay)
      {
        seedAndCropListInProcess.Add(i);
        staminaNeededInProcessTomorrow += i.GetStaminaNeeded();
      }
      seedListForNextDay = new List<SeedAndCropStack>();
      OnRecalculatingStaminaNeeded?.Invoke(staminaNeededInProcessTomorrow);
      ReduceTodayWorkerStamina(staminaUsedInProcessToday);
    }
  }
}
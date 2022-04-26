using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Worker;

namespace Manager
{
  public class WorkerManager : MonoBehaviour
  {
    public static UnityAction<bool> OnToggleTriggered;
    public static UnityAction<bool, string> OnToggleTriggered2;
    public static UnityAction<List<WorkerData>> OnRecruitableWorkerListChanged;
    public static UnityAction<List<WorkerData>> OnRecruitedWorkerListChanged;
    private InputActionAsset _playerInput;

    [SerializeField] private int recruitedWorkerLimit = 5;
    [SerializeField] private int recruitableWorkerLimit = 5;
    private WorkerList recruitedWorkerList;
    private WorkerList recruitableWorkerList;

    private bool _isToggled = false;

    private void Awake()
    {
      _playerInput = GetComponent<PlayerInput>().actions;
    }

    private void Start()
    {
      recruitedWorkerList = new WorkerList(recruitedWorkerLimit);
      recruitableWorkerList = new WorkerList(recruitableWorkerLimit);
      OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
      OnRecruitableWorkerListChanged?.Invoke(recruitableWorkerList.Workers);
    }

    private void OnEnable()
    {
      SetInactiveControlPlayerInput(false);
    }

    private void OnDisable()
    {
      SetInactiveControlPlayerInput(true);
    }

    private void OnToggle()
    {
      _isToggled = !_isToggled;
      OnToggleTriggered?.Invoke(_isToggled);
      RenewRecruitableWorkerList();
    }

    private void RenewRecruitableWorkerList()
    {
      recruitableWorkerList.ClearWorkerList();
      recruitableWorkerList.GenerateWorkerListToFull();
      OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
      OnRecruitableWorkerListChanged?.Invoke(recruitableWorkerList.Workers);
    }

    public bool RecruitWorker(WorkerData worker)
    {
      bool res = recruitedWorkerList.AddWorker(worker);
      if (res)
      {
        if (recruitableWorkerList.RemoveWorker(worker))
        {
          OnRecruitableWorkerListChanged?.Invoke(recruitableWorkerList.Workers);
        }
        OnRecruitedWorkerListChanged?.Invoke(recruitedWorkerList.Workers);
        return true;
      }
      return false;
    }

    public bool FireWorker(WorkerData worker)
    {
      bool res = recruitedWorkerList.RemoveWorker(worker);
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
  }
}
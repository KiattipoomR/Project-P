using UnityEngine;
using Manager;

namespace UI
{
  public class RecruitedWorkerPanelListUI : WorkerPanelListUI
  {
    private void OnEnable()
    {
      WorkerManager.OnRecruitedWorkerListChanged += UpdateWorkerPanelList;
      UpdateWorkerPanelList(GameManager.Instance.workerManager.RecruitedWorkerList.Workers);
    }

    private void OnDisable()
    {
      WorkerManager.OnRecruitedWorkerListChanged -= UpdateWorkerPanelList;
    }
  }
}

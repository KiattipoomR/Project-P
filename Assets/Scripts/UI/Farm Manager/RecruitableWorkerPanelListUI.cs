using UnityEngine;
using Manager;

namespace UI
{
  public class RecruitableWorkerPanelListUI : WorkerPanelListUI
  {
    public void OnEnable()
    {
      WorkerManager.OnRecruitableWorkerListChanged += UpdateWorkerPanelList;
      UpdateWorkerPanelList(GameManager.Instance.workerManager.RecruitableWorkerList.Workers);
    }

    private void OnDisable()
    {
      WorkerManager.OnRecruitableWorkerListChanged -= UpdateWorkerPanelList;
    }
  }
}

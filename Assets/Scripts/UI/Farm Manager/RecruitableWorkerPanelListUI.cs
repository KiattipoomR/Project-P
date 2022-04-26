using UnityEngine;
using Manager;

namespace UI
{
  public class RecruitableWorkerPanelListUI : WorkerPanelListUI
  {
    public void OnEnable()
    {
      WorkerManager.OnRecruitableWorkerListChanged += UpdateWorkerPanelList;
    }

    private void OnDisable()
    {
      WorkerManager.OnRecruitableWorkerListChanged -= UpdateWorkerPanelList;
    }
  }
}

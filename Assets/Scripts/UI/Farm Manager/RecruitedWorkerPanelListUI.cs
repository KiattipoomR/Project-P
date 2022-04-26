using Manager;

namespace UI
{
  public class RecruitedWorkerPanelListUI : WorkerPanelListUI
  {
    private void OnEnable()
    {
      WorkerManager.OnRecruitedWorkerListChanged += UpdateWorkerPanelList;
    }

    private void OnDisable()
    {
      WorkerManager.OnRecruitedWorkerListChanged -= UpdateWorkerPanelList;
    }
  }
}

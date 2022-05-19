using UnityEngine;
using FarmManager;
using UnityEngine.UI;
using TMPro;
using Manager;

namespace UI
{
  public class WorkerPanelUI : MonoBehaviour
  {
    [SerializeField] private string workerId;
    [SerializeField] private Image workerPanelImage;
    [SerializeField] private TextMeshProUGUI workerPanelNameText;
    [SerializeField] private TextMeshProUGUI workerPanelStaminaText;
    [SerializeField] private Toggle workerPanelsActiveToggle;

    public void SetWorker(Worker worker)
    {
      workerId = worker.WorkerId;
      workerPanelImage.sprite = worker.WorkerData.WorkerImage;
      workerPanelNameText.text = worker.WorkerData.WorkerName;
      if (workerPanelStaminaText)
      {
        workerPanelStaminaText.text = string.Format("Stamina: {0}/{1}", worker.WorkerStamina, worker.WorkerData.WorkerMaxStamina);
      }
      if (workerPanelsActiveToggle && (worker.IsActive ^ workerPanelsActiveToggle.isOn))
      {
        workerPanelsActiveToggle.SetIsOnWithoutNotify(worker.IsActive);
      }
    }

    public void RecruitThisWorker()
    {
      Debug.Log("Recruit " + workerPanelNameText.text + "!");
      GameManager.Instance.workerManager.RecruitWorkerFromRecruitableList(workerId);
    }

    public void FireThisWorker()
    {
      Debug.Log("Fire " + workerPanelNameText.text + "!");
      GameManager.Instance.workerManager.FireWorker(workerId);
    }

    public void SetWorkerIsActive()
    {
      GameManager.Instance.workerManager.SetRecruitedWorkerIsActive(workerId, workerPanelsActiveToggle.isOn);
    }
  }
}

using UnityEngine;
using Worker;
using UnityEngine.UI;
using TMPro;
using Manager;

namespace UI
{
  public class WorkerPanelUI : MonoBehaviour
  {
    [SerializeField] private WorkerData workerData;
    [SerializeField] private Image workerPanelImage;
    [SerializeField] private TextMeshProUGUI workerPanelText;

    public void SetWorker(WorkerData newWorkerData)
    {
      workerData = newWorkerData;
      workerPanelImage.sprite = workerData.WorkerImage;
      workerPanelText.text = workerData.WorkerName;
    }

    public void RecruitThisWorker()
    {
      Debug.Log("Recruit " + workerData.WorkerName + "!");
      GameManager.Instance.workerManager.RecruitWorker(workerData);
    }

    public void FireThisWorker()
    {
      Debug.Log("Fire " + workerData.WorkerName + "!");
      GameManager.Instance.workerManager.FireWorker(workerData);
    }
  }
}

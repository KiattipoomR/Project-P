using UnityEngine;
using Worker;
using System.Collections.Generic;

namespace UI
{
  public class WorkerPanelListUI : MonoBehaviour
  {
    [SerializeField] private GameObject workerPanelPrefab;
    [SerializeField] private Transform workerPanelParent;

    protected void UpdateWorkerPanelList(List<WorkerData> workers)
    {
      foreach (Transform child in workerPanelParent)
      {
        GameObject.Destroy(child.gameObject);
      }
      for (int i = 0; i < workers.Count; i++)
      {
        GameObject workerPanel = Instantiate(workerPanelPrefab, workerPanelParent);
        workerPanel.GetComponent<WorkerPanelUI>().SetWorker(workers[i]);
      }
    }

  }
}

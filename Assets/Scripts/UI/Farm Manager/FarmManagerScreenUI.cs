using Manager;
using UnityEngine;

namespace UI
{
  public class FarmManagerScreenUI : MonoBehaviour
  {
    [SerializeField] private RectTransform[] hideableObjects;

    private void Awake()
    {
      SetActiveFarmManagerPanel(false);
    }

    private void OnEnable()
    {
      WorkerManager.OnToggleTriggered += SetActiveFarmManagerPanel;
    }

    private void OnDisable()
    {
      WorkerManager.OnToggleTriggered -= SetActiveFarmManagerPanel;
    }

    private void SetActiveFarmManagerPanel(bool isActive)
    {
      foreach (RectTransform hideableObject in hideableObjects)
      {
        hideableObject.gameObject.SetActive(isActive);
      }
    }
  }
}

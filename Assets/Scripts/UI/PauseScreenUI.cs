using Manager;
using UnityEngine;

namespace UI
{
  public class PauseScreenUI : MonoBehaviour
  {
    [SerializeField] private RectTransform[] hideableObjects;

    private void Awake()
    {
      SetActiveInventoryBar(false, "Player");
    }

    private void OnEnable()
    {
      PauseManager.OnPauseTriggered += SetActiveInventoryBar;
    }

    private void OnDisable()
    {
      PauseManager.OnPauseTriggered -= SetActiveInventoryBar;
    }

    private void SetActiveInventoryBar(bool isActive, string source)
    {
      if (source != "Player") return;

      foreach (RectTransform hideableObject in hideableObjects)
      {
        hideableObject.gameObject.SetActive(isActive);
      }
    }
  }
}
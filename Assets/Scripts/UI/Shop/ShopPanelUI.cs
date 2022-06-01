using UnityEngine;

namespace UI
{
  public class ShopPanelUI : MonoBehaviour
  {
    [SerializeField] private RectTransform[] hideableObjects;

    private void Awake()
    {
      SetActiveShopPanel(false);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void SetActiveShopPanel(bool isActive)
    {
      foreach (RectTransform hideableObject in hideableObjects)
      {
        hideableObject.gameObject.SetActive(isActive);
      }
    }
  }
}

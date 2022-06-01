using UnityEngine;
using TMPro;

namespace UI
{
  public class ShopPanelUI : MonoBehaviour
  {
    [SerializeField] private RectTransform[] hideableObjects;
    [SerializeField] private TextMeshProUGUI errorText;

    private void Awake()
    {
      SetActiveShopPanel(false);
      errorText.gameObject.SetActive(false);
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

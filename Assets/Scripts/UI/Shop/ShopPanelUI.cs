using UnityEngine;
using TMPro;

namespace UI
{
  public class ShopPanelUI : MonoBehaviour
  {
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject sellPanel;
    [SerializeField] private TextMeshProUGUI errorText;

    private void Awake()
    {
      SetActiveBuyPanel(false);
      SetActiveSellPanel(false);
      errorText.gameObject.SetActive(false);
    }

    public void SetActiveBuyPanel(bool isActive)
    {
      buyPanel.SetActive(isActive);
    }

    public void SetActiveSellPanel(bool isActive)
    {
      sellPanel.SetActive(isActive);
    }
  }
}

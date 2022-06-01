using UnityEngine;
using TMPro;

namespace UI
{
  public class ShopPanelUI : MonoBehaviour
  {
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject sellPanel;

    private void Awake()
    {
      HideAll();
    }

    public void OpenBuyPanel()
    {
      buyPanel.SetActive(true);
    }

    public void OpenSellPanel()
    {
      sellPanel.SetActive(true);
    }

    public void HideAll()
    {
      foreach (GameObject g in objectsToHide)
      {
        g.SetActive(false);
      }
    }
  }
}

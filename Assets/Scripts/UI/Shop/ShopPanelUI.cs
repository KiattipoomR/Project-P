using UnityEngine;
using UnityEngine.Events;

namespace UI
{
  public class ShopPanelUI : MonoBehaviour
  {
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject sellPanel;

    public static UnityAction OnShopOpened;
    public static UnityAction OnShopClosed;

    private void Awake()
    {
      HideAll();
    }

    public void OpenBuyPanel()
    {
      buyPanel.SetActive(true);
      OnShopOpened?.Invoke();
    }

    public void OpenSellPanel()
    {
      sellPanel.SetActive(true);
      OnShopOpened?.Invoke();
    }

    public void ClosePanel()
    {
      HideAll();
      OnShopClosed?.Invoke();
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

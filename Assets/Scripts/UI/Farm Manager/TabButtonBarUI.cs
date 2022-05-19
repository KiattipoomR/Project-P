using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class TabButtonBarUI : MonoBehaviour
  {
    [SerializeField] private Transform tabParent;

    private Color inactiveColor = new Color(0.6f, 0.6f, 0.6f, 1f);
    private Color activeColor = new Color(1f, 1f, 1f, 1f);

    public void SwitchActiveButton(GameObject activeButton)
    {
      foreach (Transform childButton in transform)
      {
        childButton.GetComponent<Image>().color = inactiveColor;
      }
      activeButton.GetComponent<Image>().color = activeColor;
    }

    public void SwitchActiveTab(GameObject activeTab)
    {
      foreach (Transform childTab in tabParent)
      {
        childTab.gameObject.SetActive(false);
      }
      activeTab.gameObject.SetActive(true);
    }
  }
}


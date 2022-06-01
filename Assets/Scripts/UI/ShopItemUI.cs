using UnityEngine;
using UnityEngine.UI;
using Item;
using TMPro;
using Manager;
using Player;
using System.Collections;


namespace UI
{
  public class ShopItemUI : MonoBehaviour
  {
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField] private PlayerInventoryHolder playerInventory;

    private static int amount = 0;

    [SerializeField] private TextMeshProUGUI errorText;

    public GameObject BuyPopupPanel;
    public ItemData itemData;

    public Image artworkImage;

    public Image artworkImageBuyPopUp;

    public TextMeshProUGUI priceText;

    public TextMeshProUGUI AmountText;

    public void Buy()
    {
      bool validBuy, validInventory;
      print(itemData.BuyPrice * amount);
      validBuy = currencyManager.SubtractRune(itemData.BuyPrice * amount);
      if (validBuy)
      {
        validInventory = playerInventory.Inventory.AddToInventory(itemData, amount);
        if (validInventory)
        {
          ClosePopUpAmount();
        }
        else
        {
          ShowErrorText("Your bag is full.");
        }
      }
      else
      {
        ShowErrorText("Not have enough money");
      }
    }

    public void Sell()
    {
      bool validSell = true;
      print(itemData.SellPrice * amount);
      // Check Item here validSell = 
      if (validSell)
      {
        currencyManager.AddRune(itemData.SellPrice * amount);
        ClosePopUpAmount();
      }
      else if (!validSell)
      {
        ShowErrorText("Not have enough item");
      }
    }

    public void PopupAmount()
    {
      AmountText.text = amount.ToString();
      artworkImageBuyPopUp.sprite = itemData.ItemIcon;
      BuyPopupPanel.SetActive(true);
    }

    private void ShowErrorText(string reason)
    {
      errorText.text = reason;
      errorText.gameObject.SetActive(true);
      StartCoroutine(CloseWithDelay());
    }

    private IEnumerator CloseWithDelay()
    {
      yield return new WaitForSeconds(3);
      errorText.gameObject.SetActive(false);
    }

    public void ClosePopUpAmount()
    {
      amount = 0;
      BuyPopupPanel.SetActive(false);
    }

    public void AddAmount()
    {
      amount += 1;
      AmountText.text = amount.ToString();
    }

    public void DecreaseAmount()
    {
      if (amount - 1 < 0)
      {
        ShowErrorText("Amount cannot less than 0");
      }
      else
      {
        amount -= 1;
        AmountText.text = amount.ToString();
      }
    }

    void Start()
    {
      priceText.text = itemData.BuyPrice.ToString();
      artworkImage.sprite = itemData.ItemIcon;
    }
  }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Item;
using TMPro;
using Manager;


namespace UI
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] private CurrencyManager currencyManager;

        private static int amount = 0 ;

        public GameObject popupPanel;
        
        public GameObject BuyPopupPanel;
        public SeedData seedData;

        public Image artworkImage;
        
        public Image artworkImageBuyPopUp;

        public TextMeshProUGUI priceText;

        public TextMeshProUGUI popupMessage;
        
        public TextMeshProUGUI AmountText;

        private bool status;
        
      
        public void Buy()
        {
            print(seedData.BuyPrice * amount);
            status = currencyManager.subtraceRune(seedData.BuyPrice * amount );
            if (status != true)
            { 
                popupMessage.text = "Not have enough money";
                popupPanel.SetActive(true);
            }
            else
            {
                ClosePopUpAmount();
            }
        }
        public void PopupAmount()
        {
            AmountText.text =amount.ToString();
            artworkImageBuyPopUp.sprite = seedData.ItemIcon;
            BuyPopupPanel.SetActive(true);
            
        }

        public void ClosePopUp()
        {
            popupPanel.SetActive(false);
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
                popupMessage.text = "Amount cannot less than 0";
                popupPanel.SetActive(true);
            }
            else
            {
                amount -= 1;
                AmountText.text = amount.ToString();
            }
            
        }
        
        void Start()
        {

            popupPanel.SetActive(false);
            priceText.text = seedData.BuyPrice.ToString();
            artworkImage.sprite = seedData.ItemIcon;

        }

        
    }
}


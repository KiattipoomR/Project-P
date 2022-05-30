using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Item;
using TMPro;
using Manager;
using Player;
using Inventory;


namespace UI
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] private CurrencyManager currencyManager;
        
        [SerializeField] private PlayerInventoryHolder playerInventory;

        private static int amount = 0 ;

        public GameObject popupPanel;
        
        public GameObject BuyPopupPanel;
        public SeedData seedData;

        public Image artworkImage;
        
        public Image artworkImageBuyPopUp;

        public TextMeshProUGUI priceText;

        public TextMeshProUGUI popupMessage;
        
        public TextMeshProUGUI AmountText;

       
        
      
        public void Buy()
        {
            bool validBuy,validInventory;
            print(seedData.BuyPrice * amount);
            validBuy = currencyManager.subtraceRune(seedData.BuyPrice * amount );
            validInventory=playerInventory.Inventory.AddToInventory(seedData, amount);
            if (validBuy && validInventory)
            {
                ClosePopUpAmount();

            }else if (!validBuy)
            {
                popupMessage.text = "Not have enough money";
                popupPanel.SetActive(true);
            } else if (!validInventory )
            { 
                popupMessage.text = "Your bag is full.";
                popupPanel.SetActive(true);
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


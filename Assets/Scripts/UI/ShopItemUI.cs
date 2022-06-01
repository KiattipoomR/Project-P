using UnityEngine;
using UnityEngine.UI;
using Item;
using TMPro;
using Manager;
using Player;


namespace UI
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] private CurrencyManager currencyManager;
        
        [SerializeField] private PlayerInventoryHolder playerInventory;

        private static int amount = 0 ;

        [SerializeField] private TextMeshProUGUI errorText;
        
        public GameObject BuyPopupPanel;
        public SeedData seedData;

        public Image artworkImage;
        
        public Image artworkImageBuyPopUp;

        public TextMeshProUGUI priceText;

        
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
                ShowErrorText("Not have enough money");
            } else if (!validInventory )
            { 
                ShowErrorText("Your bag is full.");
            }
        }
        public void PopupAmount()
        {
            AmountText.text =amount.ToString();
            artworkImageBuyPopUp.sprite = seedData.ItemIcon;
            BuyPopupPanel.SetActive(true);
            
        }

        private void ShowErrorText(string reason) {
            errorText.text = reason;
            errorText.gameObject.SetActive(true);
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

            priceText.text = seedData.BuyPrice.ToString();
            artworkImage.sprite = seedData.ItemIcon;

        }

        
    }
}


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

        public GameObject popupPanel;
        public SeedData seedData;

        public Image artworkImage;

        public TextMeshProUGUI priceText;

        public TextMeshProUGUI popupMessage;

        private bool status;
        
        // Start is called before the first frame update

        public void OnClick()
        {
            status = currencyManager.subtraceRune(seedData.BuyPrice);

            if (status != true)
            {
                popupMessage.text = "Not have enough money";
                popupPanel.SetActive(true);
            }
        }

        public void ClosePopUp()
        {
            popupPanel.SetActive(false);
        }
  
        void Start()
        {

      
            priceText.text = seedData.BuyPrice.ToString();
            artworkImage.sprite = seedData.ItemIcon;

        }

        
    }
}


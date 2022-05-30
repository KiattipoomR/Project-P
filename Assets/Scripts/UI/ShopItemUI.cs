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
        

        public SeedData seedData;

        public Image artworkImage;

        public TextMeshProUGUI priceText;
        
        // Start is called before the first frame update

        public void OnClick()
        {
            print("Click");
            currencyManager.subtraceRune(seedData.BuyPrice);
        }
  
        void Start()
        {

      
            priceText.text = seedData.BuyPrice.ToString();
            artworkImage.sprite = seedData.ItemIcon;

        }

        
    }
}


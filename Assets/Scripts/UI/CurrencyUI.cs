using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Manager;

public class CurrencyUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI rune;
    
    
    private void Awake()
    {
        rune.text = CurrencyManager.getCurrentRune();
    }
    
    private void OnEnable()
    {
       rune.text = CurrencyManager.getCurrentRune();
    }
   
}

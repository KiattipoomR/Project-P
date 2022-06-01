using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Manager;

public class CurrencyUI : MonoBehaviour
{
  [Header("Components")]
  [SerializeField] private TextMeshProUGUI rune;


  private void Update()
  {
    rune.text = CurrencyManager.GetCurrentRune();
  }


}

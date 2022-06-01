using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{

  public class CurrencyManager : MonoBehaviour
  {

    [Header("Init currency")]
    [SerializeField] private int rune;

    private static int _currentRune;

    private void Awake()
    {
      _currentRune = rune;
    }

    public bool AddRune(int incomeRune)
    {
      _currentRune += incomeRune;
      return true;
    }

    public bool SubtractRune(int runeToSubtract)
    {
      if (_currentRune - runeToSubtract < 0)
      {
        return false;
      }
      else
      {
        _currentRune = _currentRune - runeToSubtract;
        return true;
      }
    }

    public static string GetCurrentRune()
    {
      return _currentRune.ToString();
    }
  }
}

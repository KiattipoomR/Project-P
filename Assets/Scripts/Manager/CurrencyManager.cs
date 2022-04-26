using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    
    [Header("Init currency")]
    
    [SerializeField] private int rune;

    private static int _currentRune;

    private void Awake()
    {
        _currentRune = rune;
       
    }
    

    private bool incomeRune(int incomeRune)
    {
        rune += incomeRune;
        return true;
    }

    private bool subtraceRune(int runeToSubtract)
    {
        if (rune - runeToSubtract < 0)
        {
            return false;
        }
        else
        {
            rune = rune - runeToSubtract;
            return true;
        }
    }

    public static string getCurrentRune()
    {
        return _currentRune.ToString();
    }
}

using UnityEngine;
using UnityEngine.Events;
using System;

public class TempTime : MonoBehaviour 
{
    public UnityEvent OnDayChanged;

    public static TempTime instance;
    public int dayCounter = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
        OnDayChanged = new UnityEvent();
    }

    public void AddDay() {
        dayCounter++;
        OnDayChanged.Invoke();
    }
}
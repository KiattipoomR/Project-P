using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Date : MonoBehaviour
{
    public TextMeshProUGUI UI_TIME_TEXT;
    public TextMeshProUGUI UI_DAY_TEXT;


    public float secPer15Min = 1;


    public GameObject Season;
    public Sprite Spring, Summer, Fall, Winter;
    private string _time;
    private string _day;


    int hr;
    int min;

    int day;

    int dayOfWeek;
    public int season;
    public int year;

    int maxHr = 24;
    int maxMin = 60;

    int maxDay = 30;
    int maxDayOfWeek = 8;
    int maxSeason = 5;
    float timer = 0;

    void Start()
    {
        hr = 7;
        min = 0;
        day = 1;
        dayOfWeek = 1;
        season = 1;
        year = 2022;
        SetTimeDate();
    }

    void Update()
    {

        if (timer >= secPer15Min)
        {
            min += 15;
            if (min >= maxMin)
            {
                min = 0;
                hr++;
                if (hr >= maxHr)
                {
                    hr = 0;
                    day++;
                    dayOfWeek++;
                    if (day >= maxDay)
                    {
                        day = 1;
                        season++;
                        if (season >= maxSeason)
                        {
                            season = 1;
                            year++;
                        }

                        SetSeason();

                    }
                    if (dayOfWeek >= maxDayOfWeek)
                    {
                        dayOfWeek = 1;
                    };
                }
            }
            timer = 0;

            SetTimeDate();


        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void SetSeason()
    {
        if (season == 1)
        {
            Season.GetComponent<Image>().sprite = Spring;
        }
        else if (season == 2)
        {
            Season.GetComponent<Image>().sprite = Summer;
        }
        else if (season == 3)
        {
            Season.GetComponent<Image>().sprite = Fall;
        }
        else
        {
            Season.GetComponent<Image>().sprite = Winter;
        }
    }

    void SetTimeDate()
    {
        if (hr <= 9)
        {
            _time = "0" + hr.ToString() + ":";
        }
        else
        {
            _time = hr.ToString() + ":";
        }
        if (min <= 9)
        {
            _time += "0" + min.ToString();
        }
        else
        {
            _time += min;
        }

        UI_TIME_TEXT.text = _time;
        if (day <= 9)
        {
            _day = "0" + day.ToString();

        }
        else
        {
            _day = day.ToString();

        }

        switch (dayOfWeek)
        {
            case 1:
                {
                    _day += " Sun";
                    break;
                }
            case 2:
                {
                    _day += " Mon";
                    break;
                }
            case 3:
                {
                    _day += " Tue";
                    break;
                }
            case 4:
                {
                    _day += " Wed";
                    break;
                }
            case 5:
                {
                    _day += " Thu";
                    break;
                }
            case 6:
                {
                    _day += " Fri";
                    break;
                }
            case 7:
                {
                    _day += " Sat";
                    break;
                }


        }
        UI_DAY_TEXT.text = _day;


    }


}

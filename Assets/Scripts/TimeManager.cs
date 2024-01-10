using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour {
    [Header("Date & Time")] [Range(1, 28)] public int dateInMonth;

    [Range(1, 4)] public int season;

    [Range(1, 99)] public int year;

    [Range(0, 24)] public int hour;

    [Range(0, 6)] public int minute;

    private DateTime DateTime;

    [Header("Tick settings")] public int TickMinutesIncrease = 10;
    public float TimeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;

    public static UnityAction<DateTime> OnDateTimeChanged;

    private void Awake() {
        DateTime = new DateTime(dateInMonth, season - 1, year, hour, minute * 10);
    }

    private void Start() {
        OnDateTimeChanged?.Invoke(DateTime);
    }

    private void Update() {
        currentTimeBetweenTicks += Time.deltaTime;

        if (currentTimeBetweenTicks >= TimeBetweenTicks) {
            currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    void Tick() {
        AdvanceTime();
    }

    void AdvanceTime() {
        DateTime.AdvanceMinute(TickMinutesIncrease);

        OnDateTimeChanged?.Invoke(DateTime);
    }
}

[System.Serializable]
public struct DateTime {
    #region Fields

    private Days day;
    private int date;
    private int year;

    private int hour;
    private int minute;

    private Season season;

    private int totalNumDays;
    private int totalNumWeeks;

    #endregion

    #region Properties

    public Days Day => day;
    public int Date => date;
    public int Hour => hour;
    public int Minute => minute;
    public Season Season => season;
    public int Year => year;
    public int TotalNumDays => totalNumDays;
    public int TotalNumWeeks => totalNumWeeks;
    public int CurrentWeek => totalNumWeeks - (totalNumWeeks / 16) * 16;

    #endregion

    #region Constructor

    public DateTime(int date, int season, int year, int hour, int minute) {
        this.day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
        this.date = date;
        this.season = (Season)season;
        this.year = year;

        this.hour = hour;
        this.minute = minute;

        totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

        totalNumWeeks = 1 + totalNumDays / 7;
    }

    #endregion

    #region Time Advancement

    public void AdvanceMinute(int minutesToAdvanceBy) {
        if (minute + minutesToAdvanceBy >= 60) {
            minute = (minute + minutesToAdvanceBy) % 60;
            AdvanceHour();
        }
        else {
            minute += minutesToAdvanceBy;
        }
    }

    private void AdvanceHour() {
        if ((hour + 1) == 24) {
            hour = 0;
            AdvanceDay();
        }
        else {
            hour++;
        }
    }

    private void AdvanceDay() {
        if (day + 1 > (Days)7) {
            day = (Days)1;
            totalNumWeeks++;
        }
        else {
            day++;
        }

        date++;

        if (date % 29 == 0) {
            AdvanceSeason();
            date = 1;
        }

        totalNumDays++;
    }

    private void AdvanceSeason() {
        if (Season == Season.Winter) {
            season = Season.Spring;
            AdvanceYear();
        }
        else {
            season++;
        }
    }

    private void AdvanceYear() {
        date = 1;
        year++;
    }

    #endregion

    #region Checks

    public bool IsNight() => hour is > 18 or < 6;

    public bool IsMorning() => hour is >= 6 and <= 12;

    public bool IsAfternoon() => hour is > 12 and < 18;

    public bool IsWeekend() => day > Days.Friday;

    public bool IsSpecificDay(Days _day) => day == _day;

    #endregion

    #region ToString

    public string DateToString() => $"{Day} {Date} {Year:D2}";

    public string TimeToString() {
        int adjustedHour = 0;

        if (hour == 0) {
            adjustedHour = 12;
        }
        else if (hour >= 13) {
            adjustedHour = hour - 12;
        }
        else {
            adjustedHour = hour;
        }

        string AmPm = hour is 0 or < 12 ? "AM" : "PM";

        return $"{adjustedHour:D2}:{minute:D2} {AmPm}";
    }
    #endregion
}

[System.Serializable]
public enum Days {
    NULL = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7,
}

[System.Serializable]
public enum Season {
    Spring = 0,
    Summer = 1,
    Autumn = 2,
    Winter = 3,
}

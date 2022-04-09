namespace GameTime
{
    [System.Serializable]
    public struct DateTime
    {
        private int _year;
        private Season _season;

        private Day _day;
        private int _date;

        private int _hour;
        private int _minute;

        private int _totalDaysPassed;

        public int Year => _year;
        public Season Season => _season;
        public Day Day => _day;
        public int Date => _date;
        public int Hour => _hour;
        public int Minute => _minute;
        public int TotalDaysPassed => _totalDaysPassed;

        public DateTime(int year, int season, int date, int hour, int minute)
        {
            _year = year;
            _season = (Season)(season - 1);

            _day = (Day)((date - 1) % 7);
            _date = date;

            _hour = hour;
            _minute = minute;

            _totalDaysPassed = date + ((int)_season * 28) + ((_year - 1) * 28 * 4);
        }

        // Returns boolean representing if a day has been advanced.
        public bool AdvanceMinute(int deltaMinutes)
        {
            _minute = (_minute + deltaMinutes) % 60;
            if (_minute == 0) AdvanceHour();

            if (!(_hour == 1 && _minute == 10)) return false;

            AdvanceDay();
            return true;
        }

        private void AdvanceHour()
        {
            _hour = (_hour + 1) % 24;
        }

        public void AdvanceDay()
        {
            _day = (Day)(((int)_day + 1) % 7);
            _date = (_date + 1) % 29;
            _totalDaysPassed++;

            _hour = 6;
            _minute = 0;

            if (_date != 0) return;

            AdvanceSeason();
            _date = 1;
        }

        private void AdvanceSeason()
        {
            _season = (Season)(((int)_season + 1) % 4);
            if (_season == 0) AdvanceYear();
        }

        private void AdvanceYear()
        {
            _year++;
        }

        public override string ToString()
        {
            return $"{TimeToString()}, {DateToString()}";
        }

        private string DateToString()
        {
            return $"{Day} {Date}, {Season} Year {Year:D2}";
        }

        private string TimeToString()
        {
            return $"{Hour:D2}:{Minute:D2}";
        }
    }

    [System.Serializable]
    public enum Day
    {
        Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat,
        Sun,
    }

    [System.Serializable]
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter,
    }
}
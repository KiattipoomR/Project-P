using DateTime = GameTime.DateTime;
using Misc;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class TimeManager : SingletonMonobehaviour<TimeManager>
    {
        [Header("Date & Time Settings")]
        [Range(1, 99)]
        [SerializeField] private int year;
        [Range(1, 4)]
        [SerializeField] private int season;
        [Range(1, 28)]
        [SerializeField] private int date;
        [Range(0, 24)]
        [SerializeField] private int hour;
        [Range(0, 6)]
        [SerializeField] private int minute;

        [Header("Tick Settings")]
        [SerializeField] private int tickMinutesIncrease = 10;
        [SerializeField] private float timeBetweenTicks = 1f;

        public UnityAction<DateTime> OnDateTimeChanged;

        private float _currentTimeBetweenTicks;
        private bool _isPaused;
        private DateTime _currentTime;

        protected override void Awake()
        {
            base.Awake();
            _currentTime = new DateTime(year, season, date, hour, minute * 10);
        }

        private void Start()
        {
            OnDateTimeChanged?.Invoke(_currentTime);
        }

        private void OnEnable()
        {
            PauseManager.Instance.OnPauseTriggered += ControlTime;
        }

        private void OnDisable()
        {
            PauseManager.Instance.OnPauseTriggered -= ControlTime;
        }

        private void Update()
        {
            if (_isPaused) return;

            _currentTimeBetweenTicks += Time.deltaTime;
            if (_currentTimeBetweenTicks < timeBetweenTicks) return;
            
            _currentTimeBetweenTicks = 0;
            TimeAdvance();
        }

        private void TimeAdvance()
        {
            _currentTime.AdvanceMinute(tickMinutesIncrease);
            OnDateTimeChanged?.Invoke(_currentTime);
        }

        private void ControlTime(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}
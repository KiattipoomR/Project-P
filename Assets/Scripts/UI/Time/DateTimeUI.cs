using Manager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DateTime = GameTime.DateTime;

namespace UI.Time
{
    public class DateTimeUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RectTransform clockFace;
        [SerializeField] private Image seasonIcon;
        [SerializeField] private TextMeshProUGUI day, date, time;

        [Header("Attributes")]
        [SerializeField] private Sprite[] seasonIcons;

        private float _startingRotation;

        private void Awake()
        {
            _startingRotation = clockFace.localEulerAngles.z;
        }

        private void OnEnable()
        {
            TimeManager.OnDateTimeChanged += UpdateDateTime;
        }

        private void OnDisable()
        {
            TimeManager.OnDateTimeChanged -= UpdateDateTime;
        }

        private void UpdateDateTime(DateTime dateTime)
        {
            day.text = dateTime.Day.ToString();
            date.text = dateTime.Date.ToString("D2");
            time.text = $"{dateTime.Hour:D2} : {dateTime.Minute:D2}";

            seasonIcon.sprite = seasonIcons[(int)dateTime.Season];

            float angle = Mathf.LerpUnclamped(0f, 360f, (dateTime.Hour - 6) / 24f);
            clockFace.localEulerAngles = new Vector3(0f, 0f, _startingRotation + angle);
        }
    }
}
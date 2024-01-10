using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class ClockManager : MonoBehaviour {
    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Year, Season, Week, Time;

    //public Image weatherSprite; TODO: Weather system
    //public Sprite[] weatherSprites;

    private float startingRotation;

    public Light2D sunlight;
    public float nightIntensity;
    public float dayIntensity;

    public AnimationCurve dayNightCurve;

    private void Awake() {
        startingRotation = ClockFace.localEulerAngles.z;
    }

    private void OnEnable() {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable() {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    private void UpdateDateTime(DateTime dateTime) {
        Date.text = dateTime.Date.ToString();
        Year.text = dateTime.Year.ToString();
        Season.text = dateTime.Season.ToString();
        Week.text = dateTime.CurrentWeek.ToString();
        Time.text = dateTime.TimeToString();
        // weatherSprite.sprite = weatherSprites[(int)WeatherManager.currentWeather]; TODO: Create weather manager

        float t = (float)dateTime.Hour / 24f;

        float newRotation = Mathf.Lerp(0, 360, t);
        ClockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);

        float dayNightT = dayNightCurve.Evaluate(t);

        sunlight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, dayNightT);
    }
}

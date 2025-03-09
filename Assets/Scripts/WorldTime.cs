using UnityEngine;

public class WorldTime : MonoBehaviour
{
    [SerializeField] private float minutesInDay;
    [SerializeField, Range(0, 24)] private float hourOfDay;
    [SerializeField] private Transform sunTransform;
    [SerializeField] private string time;

    public string Get => time;

    private float _totalRotation;

    private void Update()
    {
        hourOfDay += UnityEngine.Time.deltaTime / (60 * minutesInDay / 24);

        if (hourOfDay > 24) hourOfDay = 0 + (hourOfDay - 24);
        
        sunTransform.eulerAngles = new Vector3(15f * (hourOfDay - 6), 0f, 0f);
        var seconds = hourOfDay % 1 * 60;
        time = $"{Mathf.Floor(hourOfDay):00}:{seconds:00}";
    }

    private void OnValidate()
    {
        Update();
    }
}

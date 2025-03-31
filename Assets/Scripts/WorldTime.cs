using System;
using Unity.VisualScripting;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    [SerializeField] private float minutesInDay;
    [SerializeField, Range(0, 24)] private float hourOfDay;
    [SerializeField] private Transform sunTransform;
    [SerializeField] private string time;

    public string Get => time;

    private float _totalRotation;
    
    private int lastMinute;

    public static WorldTime Instance
    {
        get;
        private set;
    }

    public static event Action<int, int> TimeUpdated;

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("More than one world time instance");
        }
    }

    private void Update()
    {
        hourOfDay += Time.deltaTime / (60 * minutesInDay / 24);

        if (hourOfDay > 24) hourOfDay = 0 + (hourOfDay - 24);
        
        sunTransform.eulerAngles = new Vector3(15f * (hourOfDay - 6), 0f, 0f);
        var seconds = hourOfDay % 1 * 60;
        
        var currentHour = Mathf.FloorToInt(hourOfDay);
        var currentMinute = Mathf.FloorToInt((hourOfDay % 1) * 60);
        if (currentMinute != lastMinute)
        {
            lastMinute = currentMinute;
            TimeUpdated?.Invoke(currentHour, currentMinute);
        }
        
        time = $"{Mathf.Floor(hourOfDay):00}:{seconds:00}";
    }

    private void OnValidate()
    {
        Update();
    }
}

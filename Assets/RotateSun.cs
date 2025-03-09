using System;
using UnityEngine;

public class RotateSun : MonoBehaviour
{
    [SerializeField] private float minutesInDay;
    [SerializeField, Range(0, 24)] private float hourOfDay;
    [SerializeField] private Transform sunTransform;

    private float _totalRotation;

    private void Update()
    {
        hourOfDay += Time.deltaTime / (60 * minutesInDay / 24);

        if (hourOfDay > 24) hourOfDay = 0 + (hourOfDay - 24);
        
        sunTransform.eulerAngles = new Vector3(15 * (hourOfDay - 6), 0f, 0f);
    }

    private void OnValidate()
    {
        Update();
    }
}

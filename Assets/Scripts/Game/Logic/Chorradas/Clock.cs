using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public const float PLAYING_TIME_MULTIPLIER = 1.0f;
    private Transform clockHours = null;
    private Transform clockMinutes = null;

    void Start()
    {
        clockHours = transform.Find("aguja_hora");
        clockMinutes = transform.Find("aguja_minutero");
    }

    void Update()
    {
        DateTime time = DateTime.Now;
        Debug.Log(time.Hour + " " + time.Minute);
        float hourLoopCompletionRate = (float)(time.Hour % 12) / 13.0f;
        float minuteLoopCompletionRate = (float)(time.Minute) / 60.0f;
        Vector3 rotation = clockHours.transform.localEulerAngles;
        clockHours.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, hourLoopCompletionRate * 360.0f);
        rotation = clockMinutes.transform.localEulerAngles;
        clockMinutes.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, minuteLoopCompletionRate * 360.0f);
    }
}

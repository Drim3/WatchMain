using UnityEngine;
using System;

public class WatchController : MonoBehaviour
{
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand; 

    private void Update()
    {
        if (WorldTimeAPI.Instance.IsTimeLodaed)
        {
            DateTime currentTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            UpdateWatchHands(currentTime);
        }
    }

    private void UpdateWatchHands(DateTime time)
    {
        float hourAngle = (time.Hour % 12 + time.Minute / 60f) * 30f;
        float minuteAngle = (time.Minute + time.Second / 60f) * 6f;

        hourHand.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteAngle);    
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputHour;
    [SerializeField] private TMP_InputField inputMinute;

    private const string KEY_MINUTE_VALUE = "minuteValue";
    private const string KEY_HOUR_VALUE = "hourValue";

    public void ChangedInputHour(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;
       
        inputHour.text = Mathf.Clamp(int.Parse(value),0,23).ToString("00");
        PlayerPrefs.SetString(KEY_HOUR_VALUE,inputHour.text);
    }

    public void ChangedInputMinute(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;
        
        inputMinute.text = Mathf.Clamp(int.Parse(value), 0, 59).ToString("00");
        PlayerPrefs.SetString(KEY_MINUTE_VALUE, inputMinute.text);
    }

    private void OnEnable()
    {
        inputHour.text = PlayerPrefs.GetString(KEY_HOUR_VALUE);
        inputMinute.text = PlayerPrefs.GetString(KEY_MINUTE_VALUE);
    }

    private void Update()
    {
        inputHour.caretPosition = inputHour.text.Length;
        inputMinute.caretPosition = inputMinute.text.Length;
    }
}


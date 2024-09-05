using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class WorldTimeAPI : MonoBehaviour
{
    public static WorldTimeAPI Instance;

    [HideInInspector] public bool IsTimeLodaed;

    private List<string> apiUrls = new List<string>()
    {
        "https://api.ipgeolocation.io/timezone?apiKey=API_KEY&tz=Russia/Moskow",
        "http://worldtimeapi.org/api/ip"
    };

    private int currentApiIndex
    {
        get => currentApiIndexValue;
        set => currentApiIndexValue = value >= apiUrls.Count ? 0 : value;
    }
    private int currentApiIndexValue = 0;

    private DateTime currentDateTime = DateTime.Now;
     
    public DateTime GetCurrentDateTime()
    {      
        return currentDateTime.AddSeconds(Time.realtimeSinceStartup);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPI(apiUrls[currentApiIndex]));
    }

    private IEnumerator GetRealDateTimeFromAPI(string API_URL)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
        Debug.Log("getting real datetime...");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("Error: " + webRequest.error);
            currentApiIndex++;
            StartCoroutine(GetRealDateTimeFromAPI(apiUrls[currentApiIndex]));

        }
        else
        {
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
  
            currentDateTime = ParseTime(timeData.datetime);
            IsTimeLodaed = true;
            Debug.Log("Success.");
        }
    }
    
    private DateTime ParseTime(string datetime)
    {     
        string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;

        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

        return DateTime.Parse(time);
    }

    private struct TimeData
    {   
        public string datetime;
    }
}
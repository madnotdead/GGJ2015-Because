using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    public float[] time;
    private int currentTimeIndex = 0;

    public static TimeManager instance;

    private void Start()
    {
        instance = this;
    }

    public float CurrentTime
    {
        get { return time[currentTimeIndex]; }
    }

    public bool TimeLeft
    {
        get { return currentTimeIndex < time.Length; }
    }

    public void Next()
    {
        currentTimeIndex++;
    }

 
}

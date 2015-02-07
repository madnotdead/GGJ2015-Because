using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    public float[] time;
    private int currentTimeIndex = 0;
    public int countTimeFases = 3;
    public static TimeManager instance;
    public float[] timeFases;
    private float timer = 0f;
    private float fixedTimeFase= 0f;
    private bool fasesCalculated = false;

    public float CurrentTime
    {
        get { return time[currentTimeIndex]; }
    }

    public bool TimeLeft
    {
        get
        {
            return GameManager.instance.currentPlayer.audio.isPlaying;
        }
    }

    public int CurrentFase { get; private set; }

    public float CurrentFaseTime
    {
        get
        {
            return timeFases[CurrentFase];
        }
    }

    void Start()
    {
        instance = this;
        timeFases = new float[countTimeFases];
        CurrentFase = 0;
        CalculateFases();
    }

  

    void Update()
    {
        if (!fasesCalculated) CalculateFases();
        if (currentTimeIndex > timeFases.Length)
            return;

        timer += Time.deltaTime;

        if (timer <= fixedTimeFase)
            return;

        timer = 0f;

        currentTimeIndex++;
    }

    public void Next()
    {
        currentTimeIndex++;
    }

    public void CalculateFases()
    {
        if (GameManager.instance.currentPlayer != null)
        {
            var totalTime = GameManager.instance.currentPlayer.audio.time;

            fixedTimeFase = (int)totalTime / countTimeFases;

            for (var i = 0; i < timeFases.Length; i++)
            {
                timeFases[i] = fixedTimeFase;
            }
            fasesCalculated = true;
        }

    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerPuppeteer : MonoBehaviour
{
    public enum Letter
    {
        _,
        A,
        E,
        I,
        O,
        U
    };

    private List<Letter> letters;
    private List<float> times;
    public Dictionary<Letter, Sprite> mouths = new Dictionary<Letter, Sprite>();

    public static float timeOffset = 0;
    public static int currentIndex = 0;


    public Sprite[] sprites;
    void Start()
    {
        for (var i = 0; i < Enum.GetNames(typeof(Letter)).Length; i++)
        {
            string name = Enum.GetNames(typeof(Letter))[i];
            Letter targetLetter = (Letter)Enum.Parse(typeof(Letter), name);
            mouths.Add(targetLetter, sprites[i]);
        }
        
        InitLetters();
    }

    void AddLetter(Letter l, float t)
    {
        letters.Add(l);
        times.Add(t);
    }

    void Update()
    {
        bool notOnTheLastOne = currentIndex < letters.Count - 1;
        if (notOnTheLastOne)
        {
            if (times[currentIndex + 1] <= currentTime + timeOffset)
            {
                currentIndex++;
                Debug.Log(string.Format("{0} - {1}", currentTime, currentIndex));
                UpdateSingersMouths();
            }
        }
    }

    private void UpdateSingersMouths()
    {
        Letter letter = letters[currentIndex];
        Debug.Log(letter);
        currentMouth = mouths[letter];
        Debug.Log(currentMouth);
        this.BroadcastMessage("UpdateMouths");
    }

    private float currentTime
    {
        get
        {
            return this.GetComponentInChildren<AudioSource>().time;//buscar el tiempo actual de la canción en segundos
        }
    }

    public Sprite currentMouth
    {
        get;
        private set;
    }

    void InitLetters()
    {
        letters = new List<Letter>();
        times = new List<float>();
        AddLetter(Letter._, 0.0f);
        AddLetter(Letter.A, 0.2f);
        AddLetter(Letter._, 4.5f);
        AddLetter(Letter.I, 5.4f);
        AddLetter(Letter.O, 6.1f);
        AddLetter(Letter.E, 7.1f);
        AddLetter(Letter.O, 7.8f);
        AddLetter(Letter.I, 8.4f);
        AddLetter(Letter.O, 9.1f);
        AddLetter(Letter._, 9.8f);
        AddLetter(Letter.I, 10.3f);
        AddLetter(Letter.E, 10.9f);
        AddLetter(Letter.I, 11.4f);
        AddLetter(Letter.O, 12.3f);
        AddLetter(Letter._, 16.2f);
        AddLetter(Letter.I, 17.8f);
        AddLetter(Letter.O, 18.1f);
        AddLetter(Letter.E, 20.7f);
        AddLetter(Letter.O, 21.4f);
        AddLetter(Letter._, 22.7f);
        AddLetter(Letter.I, 23.6f);
        AddLetter(Letter.O, 24.1f);
        AddLetter(Letter._, 28.1f);
        AddLetter(Letter.A, 30.2f);
        AddLetter(Letter._, 34.3f);
        AddLetter(Letter.I, 35.4f);
        AddLetter(Letter.O, 36.0f);
        AddLetter(Letter.E, 37.2f);
        AddLetter(Letter.I, 37.6f);
        AddLetter(Letter._, 38.0f);
        AddLetter(Letter.I, 38.3f);
        AddLetter(Letter.A, 39.1f);
        AddLetter(Letter._, 39.9f);
        AddLetter(Letter.I, 40.3f);
        AddLetter(Letter.O, 40.6f);
        AddLetter(Letter.I, 41.4f);
        AddLetter(Letter.A, 42.1f);
        AddLetter(Letter._, 46.1f);
        AddLetter(Letter.I, 47.7f);
        AddLetter(Letter.O, 48.0f);
        AddLetter(Letter.E, 50.6f);
        AddLetter(Letter.I, 51.1f);
        AddLetter(Letter._, 52.4f);
        AddLetter(Letter.I, 53.3f);
        AddLetter(Letter.A, 53.9f);
        AddLetter(Letter._, 57.9f);
        AddLetter(Letter.A, 59.9f);
        AddLetter(Letter._, 63.2f);
        AddLetter(Letter.O, 63.6f);
        AddLetter(Letter.I, 64.0f);
        AddLetter(Letter.O, 64.6f);
        AddLetter(Letter._, 65.0f);
        AddLetter(Letter.O, 65.1f);
        AddLetter(Letter.I, 65.6f);
        AddLetter(Letter.U, 66.1f);
        AddLetter(Letter._, 66.8f);
        AddLetter(Letter.O, 69.6f);
        AddLetter(Letter.I, 70.1f);
        AddLetter(Letter.O, 70.4f);
        AddLetter(Letter._, 71.1f);
        AddLetter(Letter.O, 71.2f);
        AddLetter(Letter.I, 71.6f);
        AddLetter(Letter.U, 72.2f);
        AddLetter(Letter._, 72.9f);
        AddLetter(Letter.I, 77.2f);
        AddLetter(Letter.O, 77.9f);
        AddLetter(Letter.E, 79.0f);
        AddLetter(Letter.A, 79.4f);
        AddLetter(Letter.I, 80.2f);
        AddLetter(Letter.U, 81.0f);
        AddLetter(Letter._, 81.5f);
        AddLetter(Letter.I, 81.9f);
        AddLetter(Letter.E, 82.3f);
        AddLetter(Letter.I, 83.2f);
        AddLetter(Letter.A, 83.7f);
        AddLetter(Letter._, 87.6f);
        AddLetter(Letter.I, 89.3f);
        AddLetter(Letter.O, 89.7f);
        AddLetter(Letter.E, 92.4f);
        AddLetter(Letter.A, 92.8f);
        AddLetter(Letter._, 94.1f);
        AddLetter(Letter.I, 95.0f);
        AddLetter(Letter.U, 95.7f);
        AddLetter(Letter._, 99.5f);
        AddLetter(Letter.A, 101.6f);
        AddLetter(Letter._, 108.8f);
        AddLetter(Letter.A, 113.3f);
        AddLetter(Letter._, 120.4f);
        AddLetter(Letter.A, 125.0f);
        AddLetter(Letter._, 129.1f);
        AddLetter(Letter.A, 130.6f);
        AddLetter(Letter._, 136.0f);
    }
}

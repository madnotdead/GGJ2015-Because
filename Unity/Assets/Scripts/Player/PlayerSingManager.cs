using UnityEngine;
using System.Collections;

public class PlayerSingManager : MonoBehaviour
{
    public ParticleSystem ParticlesContainer;
    private AudioSource AudioSource;
    protected bool isSinging = false;
    private bool lastState = true;
    public Color noteColor = Color.white;
    // Use this for initialization
    void Start()
    {
        AudioSource = GetAudioSource();
    }

    private AudioSource GetAudioSource()
    {
        AudioSource =  AudioSource ?? this.GetComponent<AudioSource>();
        return AudioSource;
    }

    // Update is called once per frame
    void Update()
    {
        ParticlesContainer.startColor = noteColor;
        if (isSinging != lastState)
        {
            ParticlesContainer.gameObject.SetActive(isSinging);
            AudioSource.mute = !isSinging;
            lastState = isSinging;
        }
    }
    void PlayerSelected()
    {
        isSinging = false;
    }
    void Sing()
    {
        isSinging = true;
        if (!GetAudioSource().isPlaying)
            GetAudioSource().Play();
    }
}

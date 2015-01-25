using UnityEngine;
using System.Collections;

public class PlayerSingManager : MonoBehaviour
{
    public ParticleSystem ParticlesContainer;
    private AudioSource AudioSource;
    public SpriteRenderer MouthRenderer;
    protected bool isSinging = false;
    private bool lastState = true;
    public Color noteColor = Color.white;
    public Sprite worried;
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
        StopSinging();
    }
    void Sing()
    {
        isSinging = true;
        if (!GetAudioSource().isPlaying)
            GetAudioSource().Play();
        UpdateMouths();
    }
    void StopSinging()
    {
        isSinging = false;

        UpdateMouths();
    }

    public void UpdateMouths()
    {
        if (isSinging)
        {
            MouthRenderer.sprite = GetComponentInParent<PlayerPuppeteer>().currentMouth;
            Debug.Log(GetComponentInParent<PlayerPuppeteer>().currentMouth);
        }
        else
        {
            MouthRenderer.sprite = worried;
        }
    }
}

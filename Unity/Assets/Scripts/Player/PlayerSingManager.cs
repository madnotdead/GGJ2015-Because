using UnityEngine;
using System.Collections;

public class PlayerSingManager : MonoBehaviour
{
    public GameObject ParticlesContainer;
    private AudioSource AudioSource;
    protected bool isSinging = true;
    private bool lastState = true;
	// Use this for initialization
	void Start () {
        AudioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isSinging != lastState)
        {
            ParticlesContainer.SetActive(isSinging);
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
    }
}

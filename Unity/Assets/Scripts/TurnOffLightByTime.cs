using UnityEngine;
using System.Collections;

public class TurnOffLightByTime : MonoBehaviour
{

    public Light target;
    private float initialCount;
    public float timeToWait = 3f;


    void Start()
    {
        target.enabled = true;
        initialCount = 0f;
    }

    // Update is called once per frame


	void Update ()
	{

	    if (initialCount < timeToWait)
	    {
	        initialCount += Time.deltaTime;
	        return;
	    }

	    target.enabled = false;
	}   



	
}

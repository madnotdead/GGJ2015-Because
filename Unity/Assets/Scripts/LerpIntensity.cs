using System;
using UnityEngine;
using System.Collections;

public class LerpIntensity : MonoBehaviour
{

    public Light light;
    public float ease = 2f;
	// Update is called once per frame
	void Update ()
	{

	    light.intensity = Mathf.Lerp(1, 0, Time.deltaTime * ease);
	}
}

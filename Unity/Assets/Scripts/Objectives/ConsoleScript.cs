﻿using UnityEngine;
using System.Collections;

public class ConsoleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (this.enabled)
	    {
	        GetComponentInChildren<ParticleEmitter>().emit = true;
	    }
	}
}

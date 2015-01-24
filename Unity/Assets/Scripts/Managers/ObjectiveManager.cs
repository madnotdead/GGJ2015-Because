using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class ObjectiveAnimation
{
    public Animation anim;
    public AnimationClip clip;
}

[Serializable]
public class Objective
{
    public string text;
    public Texture2D icon;
    public Transform target;
    public ObjectiveAnimation[] animations;
    public float distance = 1;
}


public class ObjectiveManager : MonoBehaviour
{


    public Objective[] objectives;

    public static ObjectiveManager instance;
    private int currentObjective;

	// Use this for initialization
	void Start ()
	{
	    instance = this;
	    currentObjective = 0;
	}

    public Objective GetCurrenteObjective
    {
        get { return objectives[currentObjective]; }
    }

    // Update is called once per frame
	void Update ()
	{

	}
}



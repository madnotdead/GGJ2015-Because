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
    public float time;
    public KeyCode key;
}


public class ObjectiveManager : MonoBehaviour
{


    public Objective[] objectives;

    public static ObjectiveManager instance;
    private int currentObjective;
    private float currentTime;
	// Use this for initialization
	void Start ()
	{
	    instance = this;
	    currentObjective = 0;
	}

    public Objective GetCurrentObjective
    {
        get { return objectives[currentObjective]; }
    }

    // Update is called once per frame
	void Update ()
	{
	    if (GameManager.instance.currentPlayer == null) return;
        if (GetCurrentObjective.target == null) return;
	    var distance = Vector3.Distance(GameManager.instance.currentPlayer.transform.position, GetCurrentObjective.target.position);

	    if (!(distance <= GetCurrentObjective.distance)) return;
	    
        //Objective logic
	    if (!Input.GetKeyDown(GetCurrentObjective.key)) return;

	    if (currentTime < GetCurrentObjective.time)
	        currentTime += Time.deltaTime;
	    else
	        currentObjective = UnityEngine.Random.Range(0, objectives.Length);
	}
}



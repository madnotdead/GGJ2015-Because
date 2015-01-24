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

    public Objective GetCurrenteObjective
    {
        get { return objectives[currentObjective]; }
    }

    // Update is called once per frame
	void Update ()
	{
	    if (GameManager.instance.currentPlayer == null) return;

	    GetCurrenteObjective.target.gameObject.SetActive(true);

        var distance = Vector3.Distance(GameManager.instance.currentPlayer.transform.position, GetCurrenteObjective.target.position);

	    if (!(distance <= GetCurrenteObjective.distance)) return;
	    
        //Objective logic
	    if (!Input.GetKeyDown(GetCurrenteObjective.key)) return;

        Debug.Log(currentTime);
        Debug.Log(GetCurrenteObjective.time);

	    if (currentTime < GetCurrenteObjective.time)
	        currentTime += Time.deltaTime;
	    else
	    {
	        currentTime = 0;
            Debug.Log("Desactivate");
            GetCurrenteObjective.target.gameObject.SetActive(false);
	        GetCurrenteObjective.target.collider.enabled = false;
            Debug.Log(currentObjective);
            currentObjective = UnityEngine.Random.Range(0, objectives.Length);
            Debug.Log(currentObjective);
	    }
	    
	}
}



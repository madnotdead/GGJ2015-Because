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
    private float objectiveTimer = 0;
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
	    
	    if (!TimeManager.instance.TimeLeft) return;

        //Debug.Log("CurrentTime: " + TimeManager.instance.CurrentTime);
        //Debug.Log("objectiveTimer: " + objectiveTimer);

	    if (objectiveTimer < TimeManager.instance.CurrentTime)
	        objectiveTimer += Time.deltaTime;
	    else
	    {
            

            GetCurrenteObjective.target.gameObject.SetActive(true);

            if (GameManager.instance.currentPlayer == null) return;

            var distance = Vector3.Distance(GameManager.instance.currentPlayer.transform.position, GetCurrenteObjective.target.position);
            Debug.Log("distance: " + distance);
            if (!(distance <= GetCurrenteObjective.distance)) return;

            //Objective logic
           // if (!Input.GetKeyDown(GetCurrenteObjective.key)) return;

            //Debug.Log(currentTime);
            //Debug.Log(GetCurrenteObjective.time);

	        if (currentTime < GetCurrenteObjective.time)
	        {
                currentTime += Time.deltaTime;
               // GameManager.instance.currentPlayer.GetComponent<PlayerStateManager>().SetPlayerState(PlayerState.Working);
	        }
            else
            {
                currentTime = 0;
                GetCurrenteObjective.target.gameObject.SetActive(false);
                GetCurrenteObjective.target.collider.enabled = false;
                currentObjective = UnityEngine.Random.Range(0, objectives.Length);
                objectiveTimer = 0;
                TimeManager.instance.Next();
                GameManager.instance.ObjectiveCompleted();
                //GameManager.instance.currentPlayer.GetComponent<PlayerStateManager>().SetPlayerState(PlayerState.Returning);
            }
	    
	    }

	  
	}
}



using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState CurrentState = PlayerState.Idle;
    public PlayerTaskType CurrentTask = PlayerTaskType.Singing;
    public PlayerMoodTypes CurrentMood = PlayerMoodTypes.Normal;

    private Vector3 InitialPosition;

    public Animation[] animations;
    void Awake()
    {
        InitialPosition = transform.position;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        switch (CurrentState)
        {
        }
	}

    void SetActive()
    {
        switch (CurrentState)
        {
            case PlayerState.Idle:
                EnableMovement();
                break;
            case PlayerState.Active:
                EnableMovement();
                break;
            case PlayerState.Working:
                EnableMovement();
                break;
            case PlayerState.Returning:
                EnableMovement();
                CurrentState = PlayerState.Active;
                break;
            case PlayerState.Unconscious:
                break;
            default:
                break;
        }
    }
    void SetInactive()
    {
        DisableMovement();
        switch (CurrentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Active:
                ReturnToInitialPosition();
                break;
            case PlayerState.Working:
                CancelTask();
                ReturnToInitialPosition();
                break;
            case PlayerState.Returning:
                break;
            case PlayerState.Unconscious:
                break;
            default:
                break;
        }
    }


    private void EnableMovement()
    {
        this.GetComponent<PlayerMovement>().enabled = true;
    }
    private void DisableMovement()
    {
        this.GetComponent<PlayerMovement>().enabled = false;
    }

    private void CancelTask()
    {
        Debug.Log("Caneling task");
    }

    private void ReturnToInitialPosition()
    {
        CurrentState = PlayerState.Returning;
    }
}

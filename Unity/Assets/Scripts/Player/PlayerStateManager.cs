﻿using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState CurrentState = PlayerState.Idle;
    public PlayerTaskType CurrentTask = PlayerTaskType.Singing;
    public PlayerMoodTypes CurrentMood = PlayerMoodTypes.Normal;

    public Transform InitialPosition;

    public Animation[] animations;
    private PlayerMovement playerMovement;
    void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
    }
    // Use this for initialization
    void Start()
    {
        if (CurrentState == PlayerState.Idle)
        { 
            this.SendMessage("Sing", SendMessageOptions.DontRequireReceiver);
            DisableMovement();
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (CurrentState)
        {
            case PlayerState.Returning:
                if (this.GetComponent<Caminador>().camino.puntos.Count==0)
                    this.GetComponent<Caminador>().AddPunto(InitialPosition);
                break;
            default:
                break;
        }
    }
    public void PlayerIsBackToPosition()
    {
        this.GetComponent<Caminador>().camino.puntos.Clear();
        SetPlayerState(PlayerState.Idle);
        CurrentTask = PlayerTaskType.Singing;
        CurrentMood = PlayerMoodTypes.Normal;
        this.SendMessage("Sing", SendMessageOptions.DontRequireReceiver);
    }
    private void SetPlayerState(PlayerState playerState)
    {
        CurrentState = playerState;
    }

    [ContextMenu("Active")]
    void SetActive()
    {
        switch (CurrentState)
        {
            case PlayerState.Idle:
                this.SendMessage("PlayerSelected", SendMessageOptions.DontRequireReceiver);
                SetPlayerState(PlayerState.Active);
                break;
            case PlayerState.Active:
                break;
            case PlayerState.Working:
                break;
            case PlayerState.Returning:
                this.SendMessage("PlayerSelected", SendMessageOptions.DontRequireReceiver);
                SetPlayerState(PlayerState.Active);
                break;
            case PlayerState.Unconscious:
                break;
            default:
                break;
        }
    }
    [ContextMenu("Inactive")]
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

    private void PlayerSelected()
    {
        EnableMovement();
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
        Debug.Log("Canceling task");
    }

    private void ReturnToInitialPosition()
    {
        SetPlayerState(PlayerState.Returning);

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering trigger");
        if (other.collider.CompareTag("Stairs"))
        {
            Debug.Log("In  Stairs");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Leaving trigger");
    }

}

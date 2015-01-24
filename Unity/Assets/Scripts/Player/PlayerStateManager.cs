using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState CurrentState = PlayerState.Idle;
    public PlayerTaskType CurrentTask = PlayerTaskType.Singing;
    public PlayerMoodTypes CurrentMood = PlayerMoodTypes.Normal;

    private Vector3 InitialPosition;

    public Animation[] animations;
    private PlayerMovement playerMovement;
    void Awake()
    {
        InitialPosition = transform.position;
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
                transform.position = Vector3.Lerp(transform.position, InitialPosition, Time.deltaTime * playerMovement.speed);

                if (Vector3.Distance(transform.position, InitialPosition) <= 0.1f)
                {
                    CurrentState = PlayerState.Idle;
                    CurrentTask = PlayerTaskType.Singing;
                    CurrentMood = PlayerMoodTypes.Normal;
                    this.SendMessage("Sing", SendMessageOptions.DontRequireReceiver);
                }
                break;
            default:
                break;
        }
    }

    [ContextMenu("Active")]
    void SetActive()
    {
        switch (CurrentState)
        {
            case PlayerState.Idle:
                this.SendMessage("PlayerSelected", SendMessageOptions.DontRequireReceiver);
                CurrentState = PlayerState.Active;
                break;
            case PlayerState.Active:
                break;
            case PlayerState.Working:
                break;
            case PlayerState.Returning:
                this.SendMessage("PlayerSelected", SendMessageOptions.DontRequireReceiver);
                CurrentState = PlayerState.Active;
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
        CurrentState = PlayerState.Returning;

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

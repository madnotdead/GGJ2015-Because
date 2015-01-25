using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState CurrentState = PlayerState.Idle;
    public PlayerTaskType CurrentTask = PlayerTaskType.Singing;
    public PlayerMoodTypes CurrentMood = PlayerMoodTypes.Normal;

    public Transform InitialPosition;

    public Animation[] animations;
    private PlayerMovement playerMovement;

    public bool grounded = false;
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
        this.GetComponent<Caminador>().AddPunto(InitialPosition);
    }

    // Update is called once per frame
    void Update()
    {

        switch (CurrentState)
        {
            case PlayerState.Returning:
                this.GetComponent<Caminador>().enabled = true;
                break;
            default:
                break;
        }
    }
    public void PlayerIsBackToPosition()
    {
        StopReturning();
        SetPlayerState(PlayerState.Idle);
        CurrentTask = PlayerTaskType.Singing;
        CurrentMood = PlayerMoodTypes.Normal;
        this.SendMessage("Sing", SendMessageOptions.DontRequireReceiver);
    }

    private void StopReturning()
    {
        this.GetComponent<Caminador>().enabled = false;
    }
    public void SetPlayerState(PlayerState playerState)
    {
        PlayerState oldState = CurrentState;
        CurrentState = playerState;
        if (CurrentState == PlayerState.Idle)
        {
            rigidbody.isKinematic = true;
        }
        else
        {
            rigidbody.isKinematic = false;
        }
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
                StopReturning();
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
        this.grounded = false;
    }

    private void CancelTask()
    {
        //Debug.Log("Canceling task");
    }

    private void ReturnToInitialPosition()
    {
        SetPlayerState(PlayerState.Returning);
        Caminador caminador = this.GetComponent<Caminador>();

        caminador.camino.puntos.Clear();
        if (transform.position.y < InitialPosition.position.y)
        {

            var stairs = transform.parent.GetComponent<WaypointHelper>().LeftStairs;
            caminador.camino.puntos.AddRange(stairs);
            caminador.camino.puntos.Add(InitialPosition);
        }
        else
        {
            caminador.camino.puntos.Add(InitialPosition);
        }
        this.GetComponent<Caminador>().enabled = true;

    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entering trigger");
        //if (other.collider.CompareTag("Stairs"))
        //{
        //    Debug.Log("In  Stairs");
        //}
    }

    public void OnTriggerStay(Collider other)
    {
        grounded = false;
            
        if (other.collider.tag != "Stage") return;

        if(CurrentState == PlayerState.Active)
            grounded = true;

       // Debug.Log("grounded:" + grounded);
    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("Leaving trigger");
    }

}

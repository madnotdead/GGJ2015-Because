using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState CurrentState = PlayerState.Idle;
    public PlayerTaskType CurrentTask = PlayerTaskType.Singing;
    public PlayerMoodTypes CurrentMood = PlayerMoodTypes.Normal;
    public Animator playerAnimator;

    public Transform InitialPosition;

    public Animation[] animations;
    private PlayerMovement playerMovement;

    private bool canJump = true;
    public Transform pupilsTransform;
    public float eyeRange = 0.05f;

    public bool CanJump
    {
        get
        {
            return canJump;
        }
        set
        {
            canJump = value;
        }
    }

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
            case PlayerState.Idle:
                UpdateEyes();
                break;
            default:
                break;
        }

        UpdateAnimator();
    }

    private void UpdateEyes()
    {
        GameObject selectedPlayer = GameManager.instance.currentPlayer;
        float eyeTargetX = selectedPlayer != null ? eyeRange * (selectedPlayer.transform.position.x - transform.position.x) : 0;
        if(Mathf.Abs(eyeTargetX) > 0.05f) eyeTargetX = 0;
        pupilsTransform.localPosition = new Vector3(pupilsTransform.localPosition.x + ((eyeTargetX - pupilsTransform.localPosition.x) / 3), pupilsTransform.localPosition.y, pupilsTransform.localPosition.z);
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
        UpdateAnimator();
    }

    public void UpdateAnimator()
    {
        if (CurrentState == PlayerState.Idle)
        {
            playerAnimator.SetBool("IsActive", false);
            playerAnimator.SetBool("IsRunning", false);
            playerAnimator.SetBool("IsJumping", false);
            playerAnimator.SetBool("IsDead", false);
        }
        else
        {
            playerAnimator.SetBool("IsActive", true);
            playerAnimator.SetBool("IsRunning", playerMovement.IsMoving || CurrentState == PlayerState.Returning);
            playerAnimator.SetBool("IsJumping", rigidbody.velocity.y > 0.5f);
        }

        if (CurrentState == PlayerState.Returning || playerMovement.IsMoving)
        {
            bool movingToLeft = false;
            if (CurrentState == PlayerState.Returning)
            {
                var caminador = this.GetComponent<Caminador>();
                if (caminador.currentPunto != null)
                {
                    movingToLeft = (caminador.currentPunto.position.x < transform.position.x);
                }
            }
            else
            {
                movingToLeft = playerMovement.IsMovingLeft;
            }
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (movingToLeft ? -1 : 1), transform.localScale.y, transform.localScale.z);
        }

        playerAnimator.SetBool("IsDead", (CurrentState == PlayerState.Unconscious));
        playerAnimator.SetBool("IsWorking", (CurrentState == PlayerState.Working));
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
        this.CanJump = false;
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
        if (InitialPosition.position.y - transform.position.y > 0.5f)
        {

            Transform[] stairs = null;
            Transform[] leftStairs = transform.parent.GetComponent<WaypointHelper>().LeftStairs;
            Transform[] rightStairs = transform.parent.GetComponent<WaypointHelper>().RightStairs;
            var rightDistance = Vector3.Distance(transform.position, rightStairs[0].position);
            var leftDistance = Vector3.Distance(transform.position, leftStairs[0].position);
            if (rightDistance > leftDistance)
            {
                stairs = leftStairs;
            }
            else
            {
                stairs = rightStairs;
            }
            caminador.camino.puntos.AddRange(stairs);
            caminador.camino.puntos.Add(InitialPosition);
        }
        else
        {
            caminador.camino.puntos.Add(InitialPosition);
        }
        this.GetComponent<Caminador>().enabled = true;

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.collider.tag == "Stage")
        {
            CanJump = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.collider.tag == "Stage")
        {
            CanJump = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.collider.tag == "Stage")
        {
            CanJump = false;
        }
    }

}

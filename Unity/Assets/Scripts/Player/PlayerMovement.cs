using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;

    //Animator anim;

    Rigidbody playerRigidbody;

    int floorMask;

    float camRayLength = 100f;

    public bool jump = false;
    public float jumpForce = 5f;

    private PlayerStateManager stateManager;
    //Only calls if enabled
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        //anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        stateManager = GetComponent<PlayerStateManager>();
    }

    void FixedUpdate()
    {
        //Input.GetAxisRaw() defiered with normal GetAxis() method, this one allows you to accelerate o deacelerate player
        //movement automatically insted of using incremental variable like GetAxis()

        float h = Input.GetAxisRaw("Horizontal"); //Only possible values -1,0,1
        float v = Input.GetAxisRaw("Vertical"); //Only possible values -1,0,1

        Move(h, v);

        Turning();

        Jump();

        IsMovingRight = h > 0;
        IsMovingLeft = h < 0;
        IsMovingUp = v < 0;
        IsMovingDown = v > 0;

        Animating(h, v);
    }

    void Move(float h, float v)
    {     
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);//Move position relative to world space

    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {

            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(newRotation);//No need to use current rotation like rotation + newRotation
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log(string.Format("Jumping. Can Jump: {0}. KeyDown: {1}", stateManager.CanJump, Input.GetKeyDown(KeyCode.Space)));

        if (!stateManager.CanJump) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        stateManager.CanJump = false;
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce);
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;

        //anim.SetBool("IsWalking", walking);
    }

    public bool IsMovingDown;

    public bool IsMovingUp;

    public bool IsMovingLeft;

    public bool IsMovingRight;

    public bool IsMoving
    {
        get
        {
            return IsMovingDown | IsMovingRight | IsMovingLeft | IsMovingUp;
        }
    }
}


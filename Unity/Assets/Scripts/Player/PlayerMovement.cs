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
    public float jumpForce = 20f;
    //Only calls if enabled
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        //anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
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
        Debug.Log("Jumping");
        if(Input.GetKeyDown(KeyCode.Space) && jump)
            playerRigidbody.AddRelativeForce(0f,jumpForce,0f);
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;

        //anim.SetBool("IsWalking", walking);
    }
}


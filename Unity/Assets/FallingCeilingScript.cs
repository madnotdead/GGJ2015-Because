using UnityEngine;
using System.Collections;

public class FallingCeilingScript : MonoBehaviour
{

    public float force = -10f;
    public Vector3 InitialPosition= new Vector3(-2,-6,10);
	// Update is called once per frame

    private void OnEnable()
    {
        transform.position = InitialPosition;        
    }

    private void OnDisable()
    {
        transform.position = InitialPosition;        
    }

    void Update () {

        rigidbody.AddForce(0f, force,0f);
	}


    private void OnCollisionEnter(Collision col)
    {
        audio.PlayOneShot(audio.clip);
    }
}

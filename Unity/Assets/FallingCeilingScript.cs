using UnityEngine;
using System.Collections;

public class FallingCeilingScript : MonoBehaviour
{

    public float force = -10f;
    public Transform InitialPosition;
	// Update is called once per frame

    private void Start()
    {
        transform.position = InitialPosition.position;
    }

    void Update () {

        rigidbody.AddForce(0f, force,0f);
	}
}

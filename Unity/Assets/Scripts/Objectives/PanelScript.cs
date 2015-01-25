using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour {

    public Animator animator;
    public bool IsRight;
    public Quaternion initialRotation;
	// Use this for initialization
	void Start () {
        initialRotation = animator.gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("IsRight", IsRight);

        animator.SetBool("Idling", false);
	}

    public void OnDisable()
    {
        animator.gameObject.transform.rotation = initialRotation;
    }


}

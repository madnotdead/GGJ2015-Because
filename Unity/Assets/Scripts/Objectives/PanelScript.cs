using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour {

    public Animator animator;
    public bool IsRight;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("IsRight", IsRight);

        animator.SetBool("Idling", false);
	}
}

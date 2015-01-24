using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 positionOffset = new Vector3(0,6,-8);
	public float ease = 5.0f;
	private Vector3 goToPosition =new Vector3();
	public bool lookAt = true;

    public bool lockX = false;
    public bool lockY = false;
    //public bool lockZ = false;
    private Vector3 originalPosition = new Vector3();

    void Start()
    {
        originalPosition = transform.position;
    }
	// Update is called once per frame
	void FixedUpdate () {

        if (target == null) return;


		//Defino la posicion que tendra mi camara respecto del target
		goToPosition = GetRelativePosition() + positionOffset;
		//Realizo el movimiento de la camara desde mi posicion origen hacia el destino
		transform.position = Vector3.Lerp(transform.position, goToPosition, Time.deltaTime * ease);
		//Me aseguro que la camara este mirando al target
		if(lookAt)
			transform.LookAt(target);
	}

    private Vector3 GetRelativePosition()
    {
        Vector3 newVec = new Vector3();

        newVec.x = lockX ? originalPosition.x : target.position.x;
        newVec.y = lockY ? originalPosition.y : target.position.y;
        newVec.z = target.position.z;

        return newVec;
    }
}

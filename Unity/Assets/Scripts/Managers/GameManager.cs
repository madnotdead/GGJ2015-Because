using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject playerGameObject;//prefab de player
    public GameObject[] playersList;//lista de players
    public GameObject currentPlayer;//player seleccionado
    public CameraFollow cf;//camara para target
    static public GameManager instance;
    private int LastIndex = -1;
    private int index = -1;
	// Use this for initialization

    void Awake()
    {
        instance = this;
    }
	void Start ()
	{
        //Inicialmente el target de la camara es el escenario
        cf.target = GameObject.FindGameObjectWithTag("Console").transform;
        instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Alpha1))
	        index = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            index = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            index = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4))
            index = 3;

        if (Input.GetKeyDown(KeyCode.Alpha5))
            index = 4;

        if (Input.GetKeyDown(KeyCode.Alpha6))
            index = 5;

        if (Input.GetKeyDown(KeyCode.Alpha7))
            index = 6;

        if (Input.GetKeyDown(KeyCode.Alpha8))
            index = 7;

        if (Input.GetKeyDown(KeyCode.Alpha9))
            index = 8;


        if (index > -1)
        {
            if (LastIndex > -1 && LastIndex!=index && currentPlayer!=null)
            {
                InactiveCurrentPlayer();
            }
            currentPlayer = playersList[index];
            cf.target = currentPlayer.transform;

            currentPlayer.SendMessage("SetActive", SendMessageOptions.DontRequireReceiver);
            LastIndex = index;
        }
        else
        {
            cf.target = GameObject.FindGameObjectWithTag("Console").transform;
        }
	}

    public void ObjectiveCompleted()
    {
        Debug.Log("Objective completed");
        InactiveCurrentPlayer();
        index = -1;
    }

    private void InactiveCurrentPlayer()
    {
        currentPlayer.SendMessage("SetInactive", SendMessageOptions.DontRequireReceiver);
    }
}

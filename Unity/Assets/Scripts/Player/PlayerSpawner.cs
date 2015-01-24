using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject PlayerPrefab;
    public AudioClip AudioClip;
    public Color NoteColor;
    public int Index;
	// Use this for initialization
	void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn()
    {
        GameObject playerObj = GameObject.Instantiate(PlayerPrefab) as GameObject;
        playerObj.name = "Player " + Index.ToString();
        playerObj.transform.parent = transform.parent;
        playerObj.transform.position = this.transform.position;
        playerObj.transform.SetSiblingIndex(Index);

        PlayerStateManager stateManager = playerObj.GetComponent<PlayerStateManager>();
        stateManager.InitialPosition = transform;

        playerObj.GetComponent<AudioSource>().clip = AudioClip;

        PlayerSingManager singManager = playerObj.GetComponent<PlayerSingManager>();
        singManager.noteColor = NoteColor;

        GameManager.instance.playersList[Index] = playerObj;
    }
}

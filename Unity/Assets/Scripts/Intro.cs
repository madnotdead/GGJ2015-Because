using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour 
{
	public string levelName;
	public float delay;
	
	public void Awake()
	{
		Invoke("StartGame", delay);
	}
	
	public void Update()
	{
		if(Input.anyKey) StartGame();
	}
	
	public void StartGame()
	{
		Application.LoadLevel(levelName);
	}
}
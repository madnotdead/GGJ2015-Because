using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camino : MonoBehaviour 
{
	public List<Transform> puntos;

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		for (int i = 0; i < puntos.Count - 1; i++)
		{
			Gizmos.DrawLine(puntos[i].position, puntos[i+1].position);
		}
	}
}
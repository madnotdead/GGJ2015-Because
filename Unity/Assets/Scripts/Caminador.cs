using UnityEngine;
using System.Collections;

public class Caminador : MonoBehaviour 
{
	public Camino camino;
	public int puntoActual;
	public float velocidad;
	public float distanciaLlegada = 0.2f;
	public int direccion = 1;

	public void Update()
	{
		Transform punto = camino.puntos [puntoActual];
		Vector3 dir = (punto.position - transform.position).normalized;

		transform.position += dir * Time.deltaTime * velocidad;

		float dist = Vector3.Distance (transform.position, punto.position);
		if(dist > distanciaLlegada) return;
		puntoActual+=direccion;

		if(puntoActual == camino.puntos.Count || puntoActual == -1)
		{
			direccion = -direccion;
			puntoActual+=direccion;
		}
	}
}
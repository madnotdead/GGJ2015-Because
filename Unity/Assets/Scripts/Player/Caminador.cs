using UnityEngine;
using System.Collections;

public class Caminador : MonoBehaviour 
{
	public Camino camino;
    public Transform currentPunto;
	public int puntoActual;
	public float velocidad;
	public float distanciaLlegada = 0.2f;
	public int direccion = 1;

    public bool loop = true;
    public string MessageToEnd = "CaminoRecorrido";

	public void Update()
	{
        if (camino.puntos != null && camino.puntos.Count > 0)
        {
            if (puntoActual >= camino.puntos.Count)
            {
                puntoActual = camino.puntos.Count - 1;
            }
            currentPunto = camino.puntos[puntoActual];
            Vector3 dir = (currentPunto.position - transform.position).normalized;

            transform.position += dir * Time.deltaTime * velocidad;

            float dist = Vector3.Distance(transform.position, currentPunto.position);
            if (dist > distanciaLlegada) return;
            puntoActual += direccion;

            if ((puntoActual == camino.puntos.Count || puntoActual == -1) && loop)
            {
                direccion = -direccion;
                puntoActual += direccion;

            }
            else if ((puntoActual == camino.puntos.Count || puntoActual == -1) && !loop)
            {
                puntoActual = 0;
                SendMessage(MessageToEnd, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            currentPunto = null;
        }
	}
    public void AddPunto(Transform point)
    {
        if (camino.puntos == null) camino.puntos = new System.Collections.Generic.List<Transform>();
        camino.puntos.Add(point);
    }
}
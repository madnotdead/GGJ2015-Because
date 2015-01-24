using UnityEngine;
using System.Collections;

public class MovingSaw : MonoBehaviour
{

    public Transform[] nodes;
    public float distanceTotal = 0;
    public float[] distanceBetweenNodes;

    public float currentTime = 0;
    public float sawVelocity = 2;


    void Start()
    {
        if (nodes.Length < 2)
        {
            Debug.LogError("Moving Saw Error: there needs to be atleast 2 nodes assigned.");
            return;
        }

        distanceBetweenNodes = new float[nodes.Length - 1];

        for (int i = 0; i < nodes.Length - 1; i++)
        {
            distanceBetweenNodes[i] = (nodes[i].position - nodes[i + 1].position).magnitude;
            distanceTotal += distanceBetweenNodes[i];
        }
    }

    void FixedUpdate()
    {
        //if (ReplayObjects.playingReplay && currentTime > replayTime) PlayReplay();

        currentTime += Time.deltaTime;

        float distanceTraveled = currentTime * sawVelocity;
        float extraBit = distanceTraveled % distanceTotal;

        int currentNode = 0;

        while (extraBit > 0)
        {
            //Extrabit nunca se va a pasar del ultimo indice del array 
            extraBit -= distanceBetweenNodes[currentNode];

            if (extraBit > 0)currentNode++;

        }


        Vector3 difVectBetween = nodes[currentNode].position - nodes[currentNode + 1].position;

        transform.position = nodes[currentNode+1].position - difVectBetween.normalized * extraBit;
    }
}
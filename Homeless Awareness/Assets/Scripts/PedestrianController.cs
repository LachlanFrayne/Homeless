using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianController : MonoBehaviour {

    // Use this for initialization
    public bool paid = false;

   // public Transform[] points;
   
    private NavMeshAgent agent;


    public Transform[] endPoint;
    //private int pointNumber;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
        agent.autoBraking = false;
        //pointNumber = Mathf.RoundToInt(Random.Range(1, 10));
        Point();
    }

    private void Update()
    {
        //Debug.Log("Max");

        if (agent.remainingDistance < 2f)
        {
            FindObjectOfType<PedestrianSpawn>().peopleAlive -= 1;
            Destroy(gameObject);
        }
    }
    public void PaidBegger()
    {
        paid = true;
        Debug.Log("Stole Money from Max");
    }

    void Point()
    {
        agent.destination = endPoint[Random.Range(0, 9)].position;
    }
}

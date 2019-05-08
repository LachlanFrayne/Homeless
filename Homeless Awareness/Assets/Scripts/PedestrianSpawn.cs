using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawn : MonoBehaviour {

    public int peopleAlive;
    public Transform[] spawnpoints;
    public GameObject peoplePrefab;
	

    
	// Update is called once per frame
	void Update () {
        if (peopleAlive < 50) {
            Instantiate(peoplePrefab, spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
            peopleAlive++;
        }
	}
}

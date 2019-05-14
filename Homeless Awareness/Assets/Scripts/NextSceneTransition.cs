using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTransition : MonoBehaviour {

    public int sceneNumber;                 // what number scene in the build order to load next
    public float timeToTransition;          // how much time to wait before transitioning
	
	// Update is called once per frame
	void Update () {
        float time = 0;
        time += Time.deltaTime;
        if (time >= timeToTransition)
        {
            SceneManager.LoadScene(sceneNumber);
        }
	}
}

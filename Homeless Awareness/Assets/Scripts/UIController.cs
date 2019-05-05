using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public KeyCode storePanel;                                  // key code to bring up the store panel
    public bool inStore = false;                                // whether the player is viewing the store or not

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(storePanel) && inStore == false)
        {
            Debug.Log("In Store");
            // allows the player to use their mouse
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            inStore = true;
        }

        if (Input.GetKeyUp(storePanel) && inStore == true)
        {
            inStore = false;
            // allows the player to use their mouse
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

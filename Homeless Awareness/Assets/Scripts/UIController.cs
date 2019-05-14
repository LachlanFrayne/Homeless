using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public KeyCode storePanelButton;                            // key code to bring up the store panel
    public bool inStore = false;                                // whether the player is viewing the store or not
    public GameObject storePanel;                               // the actual store panel game object
    public GameObject playerCamera;                             // the game object with the player camera script on it

	// Use this for initialization
	void Start () {
        playerCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {

        // when the Store Panel Button is pressed
        if (Input.GetKeyUp(storePanelButton))
        {
            // if the player doen't have the store open, it will open it.
            if (inStore == false)
            {
                // allows the player to use their mouse
                inStore = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                storePanel.SetActive(true);
                // turn off the camera
                playerCamera.GetComponent<PlayerCamera>().enabled = false;
            }

            else if (inStore == true)
            {
                inStore = false;
                // allows the player to use their mouse
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                storePanel.SetActive(false);
                // turn on the camera
                playerCamera.GetComponent<PlayerCamera>().enabled = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public KeyCode storePanelButton;                            // key code to bring up the store panel
    public bool inStore = false;                                // whether the player is viewing the store or not
    public GameObject storePanel;                               // the actual store panel game object

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(storePanelButton)/* && inStore == false*/)
        {
            if (inStore == false)
            {
                Debug.Log("In Store");
                // allows the player to use their mouse
                inStore = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                storePanel.SetActive(true);
            }

            else if (inStore == true)
            {
                inStore = false;
                // allows the player to use their mouse
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                storePanel.SetActive(false);
            }
        }
    }
}

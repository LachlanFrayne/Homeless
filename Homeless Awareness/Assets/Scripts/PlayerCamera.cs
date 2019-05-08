using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 3.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float distanceOfRay = 100;
    public LayerMask raycastLayerMask;

    

    private EconomyController economy;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        economy = GameObject.Find("GameController").GetComponent<EconomyController>();
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        yaw = Mathf.Clamp(yaw, -90f, 90f);
        //the rotation range
        pitch = Mathf.Clamp(pitch, -60f, 90f);
        //the rotation range

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        RaycastHit hit;
        //raycast points from camera position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Max distance that item can be from other objects before raycast moves item

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, distanceOfRay, raycastLayerMask))
        {
            Debug.Log(hit.transform.name);

            if (Input.GetMouseButtonUp(0) && hit.transform.tag == "Person" && !hit.transform.GetComponent<PedestrianController>().paid)
            {
                economy.BegForMoney();
                hit.transform.GetComponent<PedestrianController>().PaidBeggar();
                Debug.Log("max");
            }
        }
    }
}




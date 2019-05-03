using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianController : MonoBehaviour {

    public bool paid = false;
    public void PaidBegger()
    {
        paid = true;
        Debug.Log("Stole Money from Max");
    }
}

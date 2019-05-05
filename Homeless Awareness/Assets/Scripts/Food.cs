using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [Header("Food Related Variables")]
    public string foodName;                             // the name of the food item
    public int foodCost;                                // how much money it costs
    public float foodNourishment;                       // how much hunger it restores
    public EconomyController economyController;         // grabs the economy controller

    public static string ConvertToDollars(float dollars)
    {
        return string.Format("{0:C}", dollars);
    }

    private void Start()
    {
        economyController = FindObjectOfType<EconomyController>();
    }

    public void BuyFood()
    {
        //Debug.Log("Bought a sandwich");
        if (economyController.playerMoney < foodCost)
        {
            Debug.Log("You don't have enough money");
            return;
        }
        else
        {
            Debug.Log("You bought a sandwich");
            economyController.playerMoney -= foodCost;
            economyController.playerMoney_float = (float)economyController.playerMoney / 100;                                   // converts to float and dollars
            economyController.playerMoney_String = ConvertToDollars(economyController.playerMoney_float);                       // convert the string to dollar formatting
            economyController.moneyTxt.text = economyController.playerMoney_String;                            // updates the UI elements
            economyController.hunger += foodNourishment;
            if (economyController.hunger >= 100)
                economyController.hunger = 100;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Time Related Variables")]
    //public float timePassedGlobal = 0.0f;            // how much time has passed during the day
    public float timePassed = 0.0f;                                         // trakcs how much time has passed since earning money
    public float minTime = 5.0f;                                            // the minimum amount of time that can pass before you earn money
    public float maxTime = 20.0f;                                           // the maximum amount of time that can pass before you earn money
    [System.NonSerialized] public float spawnTime;                          // a random number between min and max Time

    [Header("Money related variables")]
    public int playerMoney = 0;                                           // how much money the player has
    public float playerMoney_float = 0;                                           // how much money the player has
    [System.NonSerialized] public string playerMoney_String = "000";        // the string that stores the player money converted into a string
    //[System.NonSerialized] public string playerMoney_Dollars = "$000.00";   // the string that stores the player money converted into dolar formatting
    public Text moneyTxt;                                                   // the UI element that displays how much money the player has
    public int minMoney = 10;                                               // the min amount of money you can earn
    public int maxMoney = 500;                                              // the max amount of money you can earn
    [System.NonSerialized] public int earnMoney;                        // a random number between min and max Money
    [System.NonSerialized] public float earnMoney_rounded;                  // earnMoney converted to an int

    // Use this for initialization
    void Start () {
        // set the initial spawn interval and money to be earned. also rounds the money earned to the nearest 5 (5 cents)
        ChooseRandomTime();
        ChooseRandomMoney();
        moneyTxt.text = "$0.00";
        // clear the dialogue
        dialogueTXT.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        EarnMoney();
	}

    #region Passively Earning Money

    void EarnMoney ()
    {
        timePassed += Time.deltaTime;                                       // increase time as time passes
        if (timePassed >= spawnTime)                                        // once timePassed has reached spawnTime...
        {
            playerMoney += earnMoney;                                     // increase player money
            playerMoney_float = (float)playerMoney / 100;                                // divide by 100 and make it a float
            //Debug.Log("Player money = " + playerMoney);
            //playerMoney_String = playerMoney_float.ToString();                    // convert it to a string
            playerMoney_String = ConvertToDollars(playerMoney_float);     // convert the string to dollar formatting
            Debug.Log("Player money = " + playerMoney_String);
            moneyTxt.text = playerMoney_String;                            // updates the UI elements

            // all functions related to resetting
            timePassed = 0;                                                 // reset the timePassed
            ChooseRandomTime();                                             // choose a new time
            ChooseRandomMoney();                                            // choose a new money
        }
    }

    // sets a random time between the intervals
    public void ChooseRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
        Debug.Log("spawnTime = " + spawnTime);
    }

    // sets a random amount of money between the intervals
    public void ChooseRandomMoney()
    {
        earnMoney = Random.Range(minMoney, maxMoney);
        //Debug.Log("earnMoney = " + earnMoney);
        earnMoney = (int)((Mathf.Round(earnMoney / 5)) * 5);
        Debug.Log("earnMoney rounded = " + earnMoney);
    }

    // converting strings into money formatting
    public static string ConvertToDollars(float dollars)
    {
        return string.Format("{0:C}", dollars);
    }

    // debug script to just earn money
    public void EarnMoneyDEBUG()
    {
        // earn money
        playerMoney += earnMoney;                                     // increase player money
        playerMoney_float = (float)playerMoney / 100;                                // divide by 100 and make it a float
        playerMoney_String = ConvertToDollars(playerMoney_float);     // convert the string to dollar formatting
        Debug.Log("Player money = " + playerMoney_String);
        moneyTxt.text = playerMoney_String;                            // updates the UI elements

        // all functions related to resetting
        timePassed = 0;                                                 // reset the timePassed
        ChooseRandomTime();                                             // choose a new time
        ChooseRandomMoney();                                            // choose a new money
    }

    #endregion

    #region Begging for Money

    [Header("Dialogue related variables")]
    public Text dialogueTXT;                                            // text object where the strings will go
    public List<string> dialogueListPositive;                           // list of positive dialogue strings
    public List<string> dialogueListNegative;                           // list of negative dialogue strings

    #endregion
}

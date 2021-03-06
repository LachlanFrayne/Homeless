﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class EconomyController : MonoBehaviour {

    //[Header("Debugging")]
    //public KeyCode debugBegPassive;
    //public KeyCode debugBegPositive;
    //public KeyCode debugBegNegative;
    //public KeyCode buySandwich;

    //[Header("Inputs")]
    //public KeyCode getWarm;
    //public KeyCode openStore;

    [Header("Time Related Variables")]
    //public float timePassedGlobal = 0.0f;                                 // how much time has passed during the day
    public float timePassed = 0.0f;                                         // trakcs how much time has passed since earning money
    public float minTime = 5.0f;                                            // the minimum amount of time that can pass before you earn money
    public float maxTime = 20.0f;                                           // the maximum amount of time that can pass before you earn money
    [System.NonSerialized] public float spawnTime;                          // a random number between min and max Time

    [Header("Money related variables")]
    public int playerMoney = 0;                                             // how much money the player has
    public float playerMoney_float = 0;                                     // how much money the player has
    [System.NonSerialized] public string playerMoney_String = "000";        // the string that stores the player money converted into a string
    //[System.NonSerialized] public string playerMoney_Dollars = "$000.00"; // the string that stores the player money converted into dolar formatting
    public Text moneyTxt;                                                   // the UI element that displays how much money the player has
    public int minMoney = 10;                                               // the min amount of money you can earn
    public int maxMoney = 300;                                              // the max amount of money you can earn
    [System.NonSerialized] public int earnMoney;                            // a random number between min and max Money
    [System.NonSerialized] public float earnMoney_rounded;                  // earnMoney converted to an int

    [Header("Survival related variables")]
    public float hunger = 100;                                              // how hungry the player is
    public float hungerDecrease = 1;                                        // the rate that it decreases at
    public float warmth = 100;                                              // how warm the player is
    public float warmthDecrease = 1;                                        // the rate that it decreases at
    public Image hungerBar;                                                 // stores the fill bar for hunger
    public Image warmthBar;                                                 // stores the fill bar for hunger
    public PostProcessingProfile vignetteProfile;
    public Image blackPanel;
    

    // Use this for initialization
    void Start () {
        // set the initial spawn interval and money to be earned. also rounds the money earned to the nearest 5 (5 cents)
        ChooseRandomTime();
        ChooseRandomMoney();
        moneyTxt.text = "$0.00";
        dialogueTXT.text = "";                                              // clear the dialogue
        vignetteProfile.vignette.enabled = true;
        // counts the amount of strings in the dialogue lists
        //countMaxPositive = dialogueListPositive.Count;
        //countMaxNegative = dialogueListNegative.Count;
        //Debug.Log("Positive Dialogue Count: " + countMaxPositive);
        //Debug.Log("Negative Dialogue Count: " + countMaxNegative);
    }
	
	// Update is called once per frame
	void Update () {
        //EarnMoney();                      // no longer passively earning money
        // add a debug key for begging passive
        //if (Input.GetKeyUp(debugBegPassive))
        //    EarnMoneyDEBUG();
        ////if (Input.GetKeyUp(buySandwich))
        //    //BuyFood(GameObject.Find("Sandwich"));
        //if (Input.GetKeyUp(debugBegPositive))
        //    BegForMoneyPOS_Debug();
        //if (Input.GetKeyUp(debugBegNegative))
        //    BegForMoneyNEG_Debug();
        // hunger and warmth
        hunger -= hungerDecrease * Time.deltaTime;
        warmth -= warmthDecrease * Time.deltaTime;
        if (warmth >= 100)
            warmth = 100;                                                       // clamps warmth
        hungerBar.fillAmount = hunger / 100;
        warmthBar.fillAmount = warmth / 100;

        // makes the vignette more intense
        VignetteModel.Settings vignetteSettings = vignetteProfile.vignette.settings;
        vignetteSettings.intensity = 1 - ((warmth + hunger + 100)/300);
        vignetteProfile.vignette.settings = vignetteSettings;

        // makes the black panel more visible
        blackPanel.color = new Color(255, 255, 255, vignetteSettings.intensity);

        // end the game
        if (warmth <= 0 || hunger <= 0)
        {
            // Debug.Log("max");
            // load the ending scene
            this.GetComponent<NextSceneTransition>().enabled = true;
            //SceneManager.LoadScene(2);          
        }

        
    }

    #region Passively Earning Money

    public void EarnMoney ()
    {
        timePassed += Time.deltaTime;                                       // increase time as time passes
        if (timePassed >= spawnTime)                                        // once timePassed has reached spawnTime...
        {
            playerMoney += earnMoney;                                     // increase player money
            playerMoney_float = (float)playerMoney / 100;                                // divide by 100 and make it a float
            //Debug.Log("Player money = " + playerMoney);
            //playerMoney_String = playerMoney_float.ToString();                    // convert it to a string
            playerMoney_String = ConvertToDollars(playerMoney_float);     // convert the string to dollar formatting
            //Debug.Log("Player money = " + playerMoney_String);
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
        //Debug.Log("spawnTime = " + spawnTime);
    }

    // sets a random amount of money between the intervals
    public void ChooseRandomMoney()
    {
        earnMoney = Random.Range(minMoney, maxMoney);
        //Debug.Log("earnMoney = " + earnMoney);
        earnMoney = (int)((Mathf.Round(earnMoney / 5)) * 5);
        //Debug.Log("earnMoney rounded = " + earnMoney);
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
        //Debug.Log("Player money = " + playerMoney_String);
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
    //public int countMaxPositive;                                        // a count of how many strings are in the list of positive dialogue outcomes
    //public int countMaxNegative;                                        // a count of how many strings are in the list of negative dialogue outcomes

    [Header("Begging related variables")]
    public int randomSeed;                                             // a random int between 1-100
    public int minMoney_Beg = 100;                                      // the minimum amount of money you can earn with begging (in cents)
    public int maxMoney_Beg = 500;                                     // the maximum amount of money you can earn with begging (in cents)

    // actively begging for money
    public void BegForMoney()
    {
        //Debug.Log("Begging");
        randomSeed = Random.Range(0, 100);                              // 5% chance for begging to be successful
        if (randomSeed <= 5)
        {
            PositiveDialogue();                                         // play a positive dialogue
            int begMoney = Random.Range(minMoney_Beg, maxMoney_Beg);    // generates a random amount of money
            begMoney = (int)((Mathf.Round(begMoney / 5)) * 5);
            //Debug.Log("begMoney = " + begMoney);
            playerMoney += begMoney;                                    // increase player money by a random number
            playerMoney_float = (float)playerMoney / 100;               // converts to float and dollars
            playerMoney_String = ConvertToDollars(playerMoney_float);     // convert the string to dollar formatting
            //Debug.Log("Player money = " + playerMoney_String);
            moneyTxt.text = playerMoney_String;                            // updates the UI elements
            timePassed = 0;                                             // resets the timer but doesn't change the randoms already chosen
        }
        else
        {
            NegativeDialogue();
            timePassed = 0;
        }
    }

    public void BegForMoneyPOS_Debug()
    {
        PositiveDialogue();                                         // play a positive dialogue
        int begMoney = Random.Range(minMoney_Beg, maxMoney_Beg);    // generates a random amount of money
        begMoney = (int)((Mathf.Round(begMoney / 5)) * 5);
        //Debug.Log("begMoney = " + begMoney);
        playerMoney += begMoney;                                    // increase player money by a random number
        playerMoney_float = (float)playerMoney / 100;               // converts to float and dollars
        playerMoney_String = ConvertToDollars(playerMoney_float);     // convert the string to dollar formatting
                                                                      //Debug.Log("Player money = " + playerMoney_String);
        moneyTxt.text = playerMoney_String;                            // updates the UI elements
        timePassed = 0;                                             // resets the timer but doesn't change the randoms already chosen
    }

    public void BegForMoneyNEG_Debug()
    {
        NegativeDialogue();
        timePassed = 0;
    }

    // play one of the positive dialogue options
    public void PositiveDialogue()
    {
        int I = Random.Range(0, dialogueListPositive.Count);
        string S = dialogueListPositive[I];
        dialogueTXT.text = S;
    }

    // play one of the negative dialogue options
    public void NegativeDialogue()
    {
        int I = Random.Range(0, dialogueListNegative.Count);
        string S = dialogueListNegative[I];
        dialogueTXT.text = S;
    }

    #endregion

    #region Food Related Variables
    // sandwich
    //public int sandwichCost = 700;

    // prefabs in the scene that are food and store variables
    //public void BuyFood(GameObject foodItem)
    //{
    //    //Debug.Log("Bought a sandwich");
    //    Food food = foodItem.GetComponent<Food>();                                      // gets the Food script
    //    if (playerMoney < food.foodCost)
    //    {
    //        Debug.Log("You don't have enough money");
    //        return;
    //    }
    //    else
    //    {
    //        Debug.Log("Sandwich success");
    //        playerMoney -= food.foodCost;
    //        playerMoney_float = (float)playerMoney / 100;                                   // converts to float and dollars
    //        playerMoney_String = ConvertToDollars(playerMoney_float);                       // convert the string to dollar formatting
    //        moneyTxt.text = playerMoney_String;                            // updates the UI elements
    //        hunger += food.foodNourishment;
    //        if (hunger >= 100)
    //            hunger = 100;
    //    }
    //}

    #endregion

    #region Getting Warm
    //private bool isWarmingSelf = false;

    public void StartWarmingSelf()
    {
        warmthDecrease = -1f;
    }

    public void StopWarmingSelf()
    {
        warmthDecrease = 1;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour {

	public List<string> listOfMessages;								// strings of all the final messages
	public List<float> durationOfMessage;							// how long each respective string lasts for
	private int i = 0;												// the reference number
	public Text dialogueText;										// the UI text object
	public Color startColor = Color.black;							// the starting colour of the text
	public Color endColor = Color.white;							// the midway colour of the text
	private float currentTime = 0.0f;								// timer float variable
	private float messageDuration = 1.0f;							// how long to wait for
	private bool isCounting = false;								// whether the timer is running or not

	// Use this for initialization
	void Start () {
		foreach (var item in listOfMessages)
		{
			messageDuration = durationOfMessage[i];
			dialogueText.text = listOfMessages[i];					// grabs the respective string
			dialogueText.color = Color.Lerp(startColor, endColor, durationOfMessage[i]);
			isCounting = true;
			if (currentTime >= messageDuration)
			i++;
			isCounting = false;
			currentTime = 0;
		}
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if (isCounting == true)
			currentTime =+ Time.deltaTime;
	}
}

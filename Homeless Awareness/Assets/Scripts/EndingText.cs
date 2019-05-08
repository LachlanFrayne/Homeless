using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndingText : MonoBehaviour {

    public List<string> mesg;
    public List<float> times;

    public Text text;

    public bool done;
    public bool displaying;
    public float timer;
    public float disptime = 2f;
    public int i;

    public void Start()
    {
        DisplayMessage();
    }
	    private void Update()
    {
        if (done)
        {
			if (Input.anyKey)
			{
				Application.Quit();
				Debug.Log("Quitting");
			}
        }
		else
		{
			
            if (displaying)
            {
                timer += Time.deltaTime;
                text.color = Color.Lerp(text.color, Color.white, .05f);

                if (timer >= times[i])
                {
                    timer = 0;
                    HideMessage();
                }
            }
            else
            {
				if (i < mesg.Count - 1)
				{
                disptime -= Time.deltaTime;
                text.color = Color.Lerp(text.color, Color.black, .05f);

                if (disptime <= 0f)
                {
                    if (i >= mesg.Count - 1)
                        done = true;
                    else
                    {
                        i++;
                        DisplayMessage();
                        disptime = 2.0f;
                    }
                }
				}
				else 
				{done = true;
				return;}
            }
			

		}

    }


    void DisplayMessage()
    {
        text.text = mesg[i];
        displaying = true;
    }

    void HideMessage()
    {
        displaying = false;
    }
}

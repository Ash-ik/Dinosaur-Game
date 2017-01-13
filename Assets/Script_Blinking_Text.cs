using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Script_Blinking_Text : MonoBehaviour {

    public float Blink_Delay;
    public float Blink_Time;

    public bool Keep_Blinking = true;

    private float Time_Passed;
    private bool Blinked = false;

    public Text Component_To_Blink;

	// Use this for initialization
	void Start () {
        Time_Passed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float Time_Passed_Last_Frame = Time.deltaTime;

        if (Keep_Blinking == true)
        {
            Time_Passed = Time_Passed + Time_Passed_Last_Frame;
            if (Blinked == false)
            {
                if (Time_Passed > Blink_Delay)
                {
                    if (Component_To_Blink != null)
                        Component_To_Blink.enabled = false;
                    Blinked = true;
                    Time_Passed = 0;
                }
            }
            else
            {
                if (Time_Passed > Blink_Time)
                {
                    if (Component_To_Blink != null)
                        Component_To_Blink.enabled = true;
                    Blinked = false;
                    Time_Passed = 0;
                }
            }
        }
        else
        {
            if (Component_To_Blink != null)
            {
                if (Component_To_Blink.enabled == false)
                    Component_To_Blink.enabled = true;
            }
        }
	}
}

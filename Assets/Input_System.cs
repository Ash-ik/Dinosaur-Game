using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Input_System : MonoBehaviour {

    public Dino_Animation Dino_Animation_Script;
    public Game_System Game_Sys;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {



	    if (Dino_Animation_Script != null)
        {
			if (Game_Sys.Current_State == 0)
                return;


            if (Input.GetKey(KeyCode.Escape))
            {
					Application.Quit();
            }
         

            Dino_Animation_Script.Bow_Pressed = Input.GetKey(KeyCode.DownArrow);

            if (Input.GetKey(KeyCode.DownArrow) == true)
            {
                Dino_Animation_Script.Do_Bow();
            }
			else if (Input.GetKey(KeyCode.UpArrow) == true)
            {
                Dino_Animation_Script.Do_Jump();
            }
            else
            {
                if (Game_Sys.Bow_Down == false && Game_Sys.Is_Game_Over == false)
                {
                    Dino_Animation_Script.Do_Run();
                }
            }
        }
	}
}

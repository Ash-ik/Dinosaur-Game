using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_System : MonoBehaviour {

    public int Current_State;

    public Terrain_Movement Terrain_Script;
    public GameObject Dino;
    public GameObject Dino_Image;
    public GameObject Main_Menu;
	public Button btnUp;
	public Button btnDown;
	public GameObject TapToStartRestart;
    public GameObject Blink_Text_Overlap;
    public GameObject Score_Overlap;
    public GameObject Pause_Overlap;
    public Dino_Animation Dino_Anim;
    public bool Bow_Down = false;
    public GameObject GameOver_Overlap;
    public bool Is_Game_Over = false;
    public Text HighScore_Text;
    public AudioSource Background_Audio;
	public Text Score_Text;
	public Text My_Score;
    public float Added_Speed = 0;

	// Use this for initialization
	void Start () {
        Current_State = 0;
        Perform_Current_State();
        if (PlayerPrefs.HasKey("Highscore") == true)
        {
            int H = PlayerPrefs.GetInt("Highscore");
            HighScore_Text.text = "HI " + H.ToString("D6");
        }
        else
        {
            HighScore_Text.text = "HI 000000";
        }
    }

    void Perform_Current_State()
    {
        if (Current_State == 0)
        {
			btnUp.gameObject.SetActive (false);
			btnDown.gameObject.SetActive (false);
			TapToStartRestart.SetActive (false);
			Terrain_Script.enabled = false;
            Dino.SetActive(false);
        }
        else if (Current_State == 1)
        {
			btnUp.gameObject.SetActive (false);
			btnDown.gameObject.SetActive (false);
            Dino.SetActive(true);
			TapToStartRestart.SetActive (true);
			Dino_Image.GetComponent<Dino_Animation>().Starting = true;
            Dino_Image.GetComponent<Dino_Animation>().Dino_Animation_Type = 0;
            Dino_Image.GetComponent<Dino_Animation>().Jumped = false;
            Dino_Image.GetComponent<Dino_Animation>().Jumping = false;
            Dino_Image.GetComponent<Dino_Animation>().Y_Acceleration = 0;
            Dino_Image.GetComponent<Dino_Animation>().Score_Value = 0;
            Dino_Image.GetComponent<Dino_Animation>().Current_Score.text = "000000";

            Terrain_Script.Visible_All();

            Vector3 p_pos = Dino.transform.localPosition;
            p_pos.y = -30;
            Dino.transform.localPosition = p_pos;

            Terrain_Script.Move_Terrain = false;
            Terrain_Script.Create_Objects = false;
            Terrain_Script.enabled = true;
            Blink_Text_Overlap.SetActive(true);
            Main_Menu.SetActive(false);
        }
        else if (Current_State == 2)
        {
			TapToStartRestart.SetActive (false);
			btnUp.gameObject.SetActive (true);
			btnDown.gameObject.SetActive (true);
            Pause_Overlap.SetActive(false);
            Terrain_Script.Move_Terrain = true;
            Terrain_Script.Create_Objects = true;
            Dino_Image.GetComponent<Dino_Animation>().enabled = true;
        }
        else if (Current_State == 3)
        {
			TapToStartRestart.SetActive (true);
			btnUp.gameObject.SetActive (false);
			btnDown.gameObject.SetActive (false);
            Pause_Overlap.SetActive(true);
            Terrain_Script.Move_Terrain = false;
            Terrain_Script.Create_Objects = false;
            Dino_Image.GetComponent<Dino_Animation>().enabled = false;
        }
        else if (Current_State == 4)
        {
			TapToStartRestart.SetActive (true);
			btnUp.gameObject.SetActive (false);
			btnDown.gameObject.SetActive (false);
            GameOver_Overlap.SetActive(true);
            Terrain_Script.Move_Terrain = false;
            Terrain_Script.Create_Objects = false;

			Score_Overlap.SetActive (false);


            if (PlayerPrefs.HasKey("Highscore") == false)
            {
                PlayerPrefs.SetInt("Highscore", Dino_Image.GetComponent<Dino_Animation>().Score_Value);
            }
            else
            {
                int S_Val = PlayerPrefs.GetInt("Highscore");
                int Cur_Val = Dino_Image.GetComponent<Dino_Animation>().Score_Value;
                if (Cur_Val > S_Val)
                {
                    PlayerPrefs.SetInt("Highscore", Cur_Val);
                }
            }

            int New_H = PlayerPrefs.GetInt("Highscore");
            HighScore_Text.text = "HI " + New_H.ToString("D6");
            
			My_Score.text = "Score "+Dino_Image.GetComponent<Dino_Animation> ().Score_Value.ToString("D6");
			Score_Text.text=HighScore_Text.text;

            Dino_Image.GetComponent<Dino_Animation>().Do_Die();

            //Save highscore
            
            //Update highscore
        }
    }

    public void Screen_Starting()
    {
        Blink_Text_Overlap.SetActive(false);
        Score_Overlap.SetActive(true);
    }

    public void New_Game_Click()
    {
		if (Current_State == 0)
        {
            Current_State = 1;
            Perform_Current_State();
        }
    }

    public void Start_Game()
    {
		if (Current_State == 1)
        {
            Current_State = 2;
            Perform_Current_State();
        }
    }

    public void Touch_Down_Up()
    {
        if (Current_State == 2)
        {
            Dino_Anim.Bow_Pressed = false;
            Bow_Down = false;
        }
    }

    public void Game_Over()
    {
        if (Current_State == 2)
        {
            Background_Audio.Stop();
            Bow_Down = false;
            Dino_Anim.Bow_Pressed = false;
            Is_Game_Over = true;
            Current_State = 4;
            Perform_Current_State();
        }
    }

    public void Replay_Game()
    {
        if (Current_State == 4)
        {
            Added_Speed = 0;
            Background_Audio.loop = true;
            Background_Audio.Play();
            Is_Game_Over = false;
            Current_State = 1;
            Terrain_Script.Clear_All();
            GameOver_Overlap.SetActive(false);
            Current_State = 1;

            Perform_Current_State();
        }
    }

    public void Touch_Down_Pressed()
    {
		if (Current_State == 4) 
		{
			Replay_Game ();
		}
		else if (Current_State == 3)
        {
            Current_State = 2;
            Perform_Current_State();
        }
        else if (Current_State == 2)
        {
            Bow_Down = true;
            Dino_Anim.Bow_Pressed = true;
            
            Dino_Anim.Do_Bow();
        }
        else if (Current_State == 1)
        {
            Bow_Down = false;
            Dino_Anim.Do_Jump();
        }
    }

    public void Touch_Up()
    {
		if (Current_State == 4) 
		{
			Replay_Game ();
		}
        else if (Current_State == 3)
        {
            Current_State = 2;
            Perform_Current_State();
        }
        else if (Current_State == 2)
        {
            Dino_Anim.Bow_Pressed = false;
            Bow_Down = false;
            Dino_Anim.Do_Jump();
        }
        else if (Current_State == 1)
        {
            Bow_Down = false;
            Dino_Anim.Do_Jump();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (Current_State == 2)
        {
            Current_State = 3;
            Perform_Current_State();
        }
    }

    public void Pause_Game()
    {
        if (Current_State == 2)
        {
            Current_State = 3;
            Perform_Current_State();
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

}

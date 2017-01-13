using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dino_Animation : MonoBehaviour {

    public Sprite[] Dino_Run;
    public Sprite[] Dino_Bow;
    public Sprite[] Dino_Jump;
    public Sprite[] Dino_Die;
	public Camera mainCamera;
    public float Animation_Tick;
    public float Animation_Jump_Tick;

    public Image Dino_Image;

    public RectTransform Dino_Transform;

    public float Landscape_Level = -30;
    public float Max_Level = 80;
    public float Jump_Speed = 1;

    private float Time_Passed = 0;
    private int Current_Frame = 0;
    public bool Jumped = false;
    public bool Jumping = false;
    public float Y_Acceleration;

    public int Dino_Animation_Type = 0;

    public bool Bow_Pressed = false;
    public bool Starting = false;

    public Game_System Game_Sys;
    public Text Current_Score;
    public int Score_Value;
    private float Score_Time_Passed = 0;

    public AudioSource Jump_Audio;
    public AudioSource Hit_Audio;

	// Use this for initialization
	void Start () {
        Current_Score.text = "000000";
        Score_Value = 0;
	}

    public void Do_Run()
    {
        if (Game_Sys.Is_Game_Over == true) return;
        if (Starting == true) return;
        if (Dino_Animation_Type != 0 && Dino_Animation_Type != 2)
        {
            Dino_Animation_Type = 0;
            Current_Frame = 0;
        }
    }

    public void Do_Bow()
    {
        if (Game_Sys.Is_Game_Over == true) return;
        if (Starting == true) return;
        if (Dino_Animation_Type != 1 && Dino_Animation_Type != 2)
        {
            Dino_Animation_Type = 1;
            Current_Frame = 0;
            this.GetComponent<BoxCollider2D>().size = new Vector2(50, 43);
            this.GetComponent<BoxCollider2D>().offset = new Vector2(0, -43);
        }
    }

    public void Do_Die()
    {
        if (Dino_Animation_Type != 3)
        {
            Dino_Animation_Type = 3;
            Current_Frame = 0;
			Handheld.Vibrate();
            //Y_Acceleration = 0;
            //Jumping = false;
            //Jumped = false;
        }
    }

    public void Do_Jump()
    {
        if (Game_Sys.Is_Game_Over == true) return;
        if (Starting == true)
        {
            Game_Sys.Screen_Starting();
        }
        if (Dino_Animation_Type != 2)
        {
            Dino_Animation_Type = 2;
            Current_Frame = 0;
            Jumped = false;
            Jumping = true;
            Y_Acceleration = 13;

            Jump_Audio.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        Time_Passed = Time_Passed + Time.deltaTime;

        if (Game_Sys.Current_State == 2)
        {
            Score_Time_Passed = Score_Time_Passed + Time.deltaTime;

            if (Score_Time_Passed > 0.5)
            {
                Score_Time_Passed = Score_Time_Passed - 0.5f;
                Score_Value = Score_Value + 1;
                Current_Score.text = Score_Value.ToString("D6");
            }
        }

        if (Jumping == true)
        {
            Vector3 Pos = Dino_Transform.localPosition;
            Pos.y = Pos.y + Y_Acceleration;

           Y_Acceleration = Y_Acceleration - Time.deltaTime * Jump_Speed;
           //if (Y_Acceleration < -5f)
           //     Y_Acceleration = -5f;

            if (Pos.y > Max_Level)
            {
                Pos.y = Max_Level;
            }

            if (Pos.y < Landscape_Level)
                Pos.y = Landscape_Level;

            Dino_Transform.localPosition = Pos;
        }

        if (Dino_Animation_Type != 2)
        {
            if (Time_Passed > Animation_Tick)
            {
                Time_Passed = 0;
                if (Starting == false)
                    Current_Frame = Current_Frame + 1;
            }
        }
        else if (Dino_Animation_Type == 2)
        {
            if (Time_Passed > Animation_Jump_Tick)
            {
                if (Jumped == false)
                {
                    Time_Passed = 0;
                    Current_Frame = Current_Frame + 1;
                }
            }
        }

        if (Dino_Animation_Type == 0)
        {
            if (Current_Frame >= Dino_Run.Length)
                Current_Frame = 0;
        }
        else if (Dino_Animation_Type == 1)
        {
            if (Current_Frame >= Dino_Bow.Length)
                Current_Frame = 0;
        }
        else if (Dino_Animation_Type == 2)
        {
            if (Current_Frame >= Dino_Jump.Length)
            {
                if (Jumped == false)
                {
                    Current_Frame = 0;
                    Jumped = true;
                    Jumping = false;
                    Y_Acceleration = 0;

                    this.GetComponent<BoxCollider2D>().size = new Vector2(50, 95);
                    this.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);

                    if (Bow_Pressed == false)
                     {
                        Dino_Animation_Type = 0;
                    }
                    else
                    {
                        Dino_Animation_Type = 1;
                    }

                    if (Starting == true)
                    {
                        Starting = false;
                        Game_Sys.Start_Game();
                    }
                }
            }
        }
        else if (Dino_Animation_Type == 3)
        {
            if (Current_Frame >= Dino_Die.Length)
                Current_Frame = 0;
        }

        if (Dino_Image != null)
        {
            if (Dino_Animation_Type == 0)
                Dino_Image.sprite = Dino_Run[Current_Frame];
            else if (Dino_Animation_Type == 1)
                Dino_Image.sprite = Dino_Bow[Current_Frame];
            else if (Dino_Animation_Type == 2)
                Dino_Image.sprite = Dino_Jump[Current_Frame];
            else if (Dino_Animation_Type == 3)
                Dino_Image.sprite = Dino_Die[Current_Frame];
        }

        
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Game_Sys != null)
        {
            Hit_Audio.Play();
            Game_Sys.Game_Over();
        }
    }
}

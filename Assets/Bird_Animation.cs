using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bird_Animation : MonoBehaviour {

    public Sprite[] Bird_Sprites;

    public float Animation_Tick;

    public Image Bird_Image;

    private float Time_Passed = 0;
    private int Current_Frame = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Time_Passed = Time_Passed + Time.deltaTime;

        if (Time_Passed > Animation_Tick)
        {
            Time_Passed = 0;
            Current_Frame = Current_Frame + 1;
        }

        if (Current_Frame >= Bird_Sprites.Length)
        {
            Current_Frame = 0;
        }

        if (Bird_Image != null)
            Bird_Image.sprite = Bird_Sprites[Current_Frame];
	}
}

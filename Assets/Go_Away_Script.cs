using UnityEngine;
using System.Collections;

public class Go_Away_Script : MonoBehaviour {

    public bool Go_Away = false;
    public float Go_Speed;
    public float Default_X;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Go_Away == true)
        {
            Vector3 P = transform.localPosition;
            P.x = P.x + Go_Speed * Time.deltaTime;
            transform.localPosition = P;

            if (P.x > 400)
            {
                Go_Away = false;
                Vector3 Ploc = transform.localPosition;
                Ploc.x = Default_X;
                transform.localPosition = Ploc;
                this.gameObject.SetActive(false);
            }
        }
	}
}

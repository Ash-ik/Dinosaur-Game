using UnityEngine;
using System.Collections;

public class Tree_Auto_Destroy : MonoBehaviour {

    public float X_Limit = -400;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 P = transform.localPosition;
        if (P.x < X_Limit)
        {
            Destroy(this.gameObject);
        }
	}
}

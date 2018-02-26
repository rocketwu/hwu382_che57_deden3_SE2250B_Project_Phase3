using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    static public Hero S;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    
    //for restrain=============
    //public float Bleeding = 1f;
    //private float camWidth;
    //private float camHeight;
    //for restrain=============

    [Header("Set Dynamically")]
    public float shieldLevel = 1;

    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.awake()-attempted to assign second Hero.S");
        }
        //for restrain=============
        //camHeight = Camera.main.orthographicSize;
        //camWidth = camHeight * Camera.main.aspect;
        //for restrain=============

}

	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += x * speed * Time.deltaTime;
        pos.y += y * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(y * pitchMult, x * rollMult, 0);

        

		
	}



}

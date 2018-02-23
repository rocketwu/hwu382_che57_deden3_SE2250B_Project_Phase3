using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy {
    public override void Move()
    {
        transform.position += speed * Vector3.down*Time.deltaTime;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	//protected new void Update () {
 //       base.Update();   //call Update() in superclass so that the boundcheck can be done.
 //   }

 //   private new void Awake()
 //   {
 //       base.Awake();   //call Awake() in superclass to do the necessary init.
 //       //Debug.Log("Sub awake");
 //   }

}

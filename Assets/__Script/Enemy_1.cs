using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {

    private float Direction;
    public override void Move()
    {
        if (Direction >= 0)
            transform.position += speed * (Vector3.left + Vector3.down) * Time.deltaTime;
        else
            transform.position += speed * (Vector3.right + Vector3.down) * Time.deltaTime;
    }

    // Use this for initialization
    void Start () {
        Direction = Random.Range(-1, 1);
    }
	
	// Update is called once per frame
	//protected new void Update () {
 //       base.Update();  //call Update() in superclass so that the boundcheck can be done.
	//}

 //   protected new void Awake()
 //   {
 //       base.Awake();   //call Awake() in superclass to do the necessary init.
 //       //Debug.Log("Sub awake");
 //   }

}

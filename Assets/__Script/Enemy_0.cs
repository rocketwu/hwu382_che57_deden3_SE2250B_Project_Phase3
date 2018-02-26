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
	

}

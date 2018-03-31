using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy {
	private Vector3 direction;
    public override void Move()
    {
//        transform.position += speed * Vector3.down*Time.deltaTime;
		direction=Hero.heroPosition-transform.position;
		transform.position += speed * Time.deltaTime * ((direction.normalized) * 0.5f + Vector3.down);
    }

    // Use this for initialization
    void Start () {
		
	}
	

}

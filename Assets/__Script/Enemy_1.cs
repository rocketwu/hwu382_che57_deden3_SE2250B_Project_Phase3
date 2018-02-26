using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {

    private float Direction;
    public override void Move()
    {
        if (Direction >= 0)
        {
            transform.position += speed * (Vector3.left + Vector3.down) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 15, 0);
        }
            
        else
        {
            transform.position += speed * (Vector3.right + Vector3.down) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, -15, 0);
        }
            
    }

    // Use this for initialization
    void Start () {
        Direction = Random.Range(-1, 1);
    }
	

}

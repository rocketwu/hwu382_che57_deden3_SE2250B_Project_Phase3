using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [Header("Set in Inspector")]
    public float speed = 10f;

    public BoundsCheck boundsCheck;
	float time = 0f;
    public abstract void Move();
    public void DestoryEnemy()
    {
        	Destroy(gameObject);
    }

    protected void Awake()
    {
        boundsCheck = GetComponent<BoundsCheck>();
        boundsCheck.keepOnScreen = false;
    }

    protected void Update()
    {
        Move();
		if (boundsCheck != null && !boundsCheck.isOnScreen) {
			time += Time.deltaTime;
			if(time >= 0.5f)
				DestoryEnemy ();
		}
    }


}

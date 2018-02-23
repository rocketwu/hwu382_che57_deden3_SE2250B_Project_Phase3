using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [Header("Set in Inspector")]
    public float speed = 10f;

    public BoundsCheck boundsCheck;

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
        if (!boundsCheck.isOnScreen) DestoryEnemy();
    }


}

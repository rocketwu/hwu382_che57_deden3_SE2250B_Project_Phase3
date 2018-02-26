using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour {

    [Header("Set in Inspector")]
    public float padding = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    // Use this for initialization
    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update () {
		//boun set equal to boundary hit
		// if no boundary is hit, boun is (0,0)
        Vector2 boun = BoundaryDetect();
        if (keepOnScreen)
        {
            Vector3 pos = transform.position;
			//if boundary x is not 0, change position
            if (boun.x != 0f)
            {
                pos.x = boun.x * (camWidth - padding);

            }
			//if boundary y is not 0, change position
            if (boun.y != 0f)
            {
                pos.y = boun.y * (camHeight - padding);

            }
            transform.position = pos;
            isOnScreen = true;
        }
        else
        {
			//if boundary is hit, sets on screen status to false
            if (boun != Vector2.zero) isOnScreen = false;
            else isOnScreen = true;
        }		
	}

    Vector2 BoundaryDetect()
    {
        Vector2 boundaryReached = Vector2.zero;
        if (transform.position.x > camWidth - padding)
        {
            //when the object reach the right boundary
            boundaryReached += Vector2.right;
        }
        if (transform.position.x < -camWidth + padding)
        {
            //when the obj reaches the left boundary
            boundaryReached += Vector2.left;
        }
        if (transform.position.y > camHeight - padding)
        {
            //when the object reaches the top boundary
            boundaryReached += Vector2.up;
        }
        if (transform.position.y < -camHeight + padding)
        {
			//when the object reaches the bottom boundary
            boundaryReached += Vector2.down;
        }


		//if the object reaches the right boundary, boundaryReached = (1,0)
		//if the object reaches the left boundary, boundaryReached = (-1,0)
		//if the object reaches the top boundary, boundaryReached = (0,1)
		//if the object reaches the bottom boundary, boundaryReached = (0,-1)
        return boundaryReached;
    }
}

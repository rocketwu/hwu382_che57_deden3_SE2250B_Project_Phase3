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
        Vector2 boun = BoundaryDetect();
        if (keepOnScreen)
        {
            Vector3 pos = transform.position;
            if (boun.x != 0f)
            {
                pos.x = boun.x * (camWidth - padding);

            }
            if (boun.y != 0f)
            {
                pos.y = boun.y * (camHeight - padding);

            }
            transform.position = pos;
            isOnScreen = true;
        }
        else
        {
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
            //when the obj reaches the top
            boundaryReached += Vector2.up;
        }
        if (transform.position.y < -camHeight + padding)
        {
            boundaryReached += Vector2.down;
        }



        return boundaryReached;
    }
}

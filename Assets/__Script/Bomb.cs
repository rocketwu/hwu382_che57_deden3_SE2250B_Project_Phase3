using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private BoundsCheck bc;
    public float speed = 10f;

	// Use this for initialization
	void Start () {
        bc = GetComponent<BoundsCheck>();
        transform.position = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += speed * Vector3.up * Time.deltaTime;
        transform.localScale += speed * new Vector3(1, 1, 0) * Time.deltaTime;
	}
}

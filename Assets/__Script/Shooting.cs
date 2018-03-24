using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
	private Vector3 _shootingPosition;
	public GameObject bulletPrefab;
	public float bulletSpeed;
	// Use this for initialization
	void Awake () {
		bulletSpeed = 2000f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			_shootingPosition = this.GetComponent<Transform> ().position;
			GameObject bullet = Instantiate<GameObject>(bulletPrefab);
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			bullet.transform.position = _shootingPosition;
			rb.AddForce (Vector3.up * bulletSpeed);
		}			
	}
}

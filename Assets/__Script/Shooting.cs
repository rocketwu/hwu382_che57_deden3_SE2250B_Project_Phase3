using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
	private Vector3 _shootingPosition;
	public GameObject bulletPrefab;
	public float bulletSpeed;
	// Use this for initialization
	void Awake () {
		bulletSpeed = 50f;
	}
	
	// Update is called once per frame
	void Update () {
		//simpleShooting ();
		blasterShooting ();	
	}

	private void simpleShooting(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			_shootingPosition = this.GetComponent<Transform> ().position;
			GameObject bullet = Instantiate<GameObject>(bulletPrefab);
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			bullet.transform.position = _shootingPosition;
			rb.velocity = Vector3.up * bulletSpeed;
		}
	}

	private void blasterShooting(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			_shootingPosition = this.GetComponent<Transform> ().position;
			for (float i = -1f; i <= 1f; i++) {
				GameObject bullet = Instantiate<GameObject>(bulletPrefab);
				Rigidbody rb = bullet.GetComponent<Rigidbody>();
				bullet.transform.position = _shootingPosition;
				bullet.transform.rotation = new Quaternion (0, 0, i * 0.6f, 0);
				//rb.velocity = Vector3.up * bulletSpeed;
				rb.velocity = new Vector3 (i*0.6f, 1f, 0) * bulletSpeed;
			}
		}
	}
}

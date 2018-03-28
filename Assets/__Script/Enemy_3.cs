using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy {

	private float timeCounter = 0f;
	private Vector3 _shootingPosition;
	private float fireTime = 0.9f;
	private Vector3 direction;
	private float backTime;
	public GameObject bulletPrefab;
	public float bulletSpeed;

	public override void Move()
	{
		timeCounter += Time.deltaTime;
		_shootingPosition = transform.position;
		if (timeCounter < backTime) {
//			direction = Vector3.down;
			//direction = Vector3.MoveTowards(transform.position,Hero.heroPosition,1000f)*0.1f;
			direction=Hero.heroPosition-transform.position;
			transform.position += speed * Time.deltaTime * (direction.normalized);
		} else {
			fireTime += Time.deltaTime;
			transform.position += speed * Vector3.up * Time.deltaTime * 0.5f;
			if (fireTime >= 1f) {
				fire ();
				fireTime = 0f;
			}

		}
	}

	// Use this for initialization
	void Start () {
		direction = Vector3.MoveTowards(transform.position,Hero.heroPosition,50f);
		backTime = Random.value + Random.Range(0,2);
	}

	private void fire(){
		for (int i = -1; i <= 1; i += 2) {
			_shootingPosition = this.GetComponentInParent<Transform> ().position + 1f * Vector3.right * i;
			GameObject bullet = Instantiate<GameObject> (bulletPrefab);
			Rigidbody rb = bullet.GetComponent<Rigidbody> ();
			bullet.transform.position = _shootingPosition;
			rb.velocity = Vector3.down * bulletSpeed;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy {
	private Vector3 direction;
	private float counter = 0f;
	public float fireTimeGap;
	public float chasingTimeGap;
	public GameObject bulletPrefab;
	private Vector3 _shootingPosition;
//	private int fireCounter = 0;
	private float fireTimeCounter = 0f;
	public float bulletSpeed;
	public float backTimeGap;

	// Use this for initialization

	// Update is called once per frame


	private void chaseHero() {
		direction=Hero.heroPosition-transform.position;
		transform.position += speed * Time.deltaTime * (direction.normalized);
	}

	private void fire() {
		for (int i = -4; i <= 4; i += 2) {
			_shootingPosition = this.GetComponentInParent<Transform> ().position + 1f * Vector3.right * i;
			GameObject bullet = Instantiate<GameObject> (bulletPrefab);
			Rigidbody rb = bullet.GetComponent<Rigidbody> ();
			bullet.transform.position = _shootingPosition;
			rb.velocity = Vector3.down * bulletSpeed;
		}
	}

	public override void Move ()
	{
		counter += Time.deltaTime;
		if (counter <= chasingTimeGap) {
			chaseHero ();
		} else{
			fireTimeCounter += Time.deltaTime;
			direction=Hero.heroPosition-transform.position;
			transform.position += speed * Time.deltaTime * (direction.normalized) + 0.25f * Vector3.up;
			if (counter > chasingTimeGap + backTimeGap) {
				counter = 0;
			}
			if (fireTimeCounter >= fireTimeGap) {
				fire ();
				fireTimeCounter = 0;
			}
		}
		
	}
}

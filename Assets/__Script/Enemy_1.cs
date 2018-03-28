using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {

	private Vector3 _shootingPosition;
	private float fireTime = 0.5f;
	public GameObject bulletPrefab;
	public float bulletSpeed;
    private float Direction;
	private float timeCounter = 0.4f;

    public override void Move()
    {
		timeCounter += Time.deltaTime;
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
		if (timeCounter >= fireTime) {
			fire ();
			timeCounter = 0;
		}
    }

    // Use this for initialization
    void Start () {
        Direction = Random.Range(-1, 1);
    }
	
	private void fire(){
		_shootingPosition = this.GetComponentInParent<Transform> ().position;
		GameObject bullet = Instantiate<GameObject>(bulletPrefab);
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.position = _shootingPosition;
		rb.velocity = Vector3.down * bulletSpeed;
	}
}

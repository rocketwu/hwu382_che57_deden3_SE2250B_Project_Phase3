using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private BoundsCheck boundsCheck;
	private float time = 0f;
	private GameObject lastContact;


	// Use this for initialization
	void Awake () {
		boundsCheck = GetComponent<BoundsCheck> ();
		boundsCheck.keepOnScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
		CheckBound ();
	}

	void CheckBound() {
		if (boundsCheck != null && !boundsCheck.isOnScreen) {
			time += Time.deltaTime;
			if(time >= 0.1f)
				Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (lastContact== other.transform.root.gameObject)
		{
			return;
		}

		lastContact = other.transform.root.gameObject;//.GetComponentInParent<GameObject>();
		if (lastContact.tag=="Enemy")
		{
			Destroy(this.gameObject);//maybe not?
			other.gameObject.GetComponentInParent<Enemy>().enemyHp--;
		}
	}	
}

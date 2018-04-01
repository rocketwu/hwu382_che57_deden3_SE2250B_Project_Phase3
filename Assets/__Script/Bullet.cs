using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private BoundsCheck boundsCheck;
	private float time = 0f;
	private GameObject lastContact;
	private Renderer rend;

	[Header("Set Dynamically")]
	public Rigidbody rigid;
	[SerializeField]
private weaponType _type;
public weaponType type {
get { return(_type);}
set { SetType (value); }

}

	// Use this for initialization
	void Awake () {
		boundsCheck = GetComponent<BoundsCheck> ();
		boundsCheck.keepOnScreen = false;
		rend = GetComponent<Renderer> ();
		rigid = GetComponent<Rigidbody> ();
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

	public void SetType (weaponType eType){
	
		_type = eType;
		WeaponDefinition def = Main.GetWeaponDefinition (_type);
		rend.material.color = def.projectileColor;
	}
}

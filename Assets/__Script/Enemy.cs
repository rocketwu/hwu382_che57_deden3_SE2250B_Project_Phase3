using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public static List<GameObject> enemies;

    [Header("Set in Inspector")]
    public float speed;
	public int enemyHp;
	public int enemySc;
	private GameObject lastContact;
    public BoundsCheck boundsCheck;
	float time = 0f;

    public abstract void Move();

    public void DestoryEnemy()
    {
        enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    protected void Awake()
    {
        boundsCheck = GetComponent<BoundsCheck>();
        boundsCheck.keepOnScreen = false;
    }

    protected void Update()
    {
        Move();
		CheckHp ();
		if (boundsCheck != null && !boundsCheck.isOnScreen) {
			time += Time.deltaTime;
			if(time >= 0.5f)
				DestoryEnemy ();
		}
    }

	protected void CheckHp ()
	{
		if (enemyHp <= 0) {
			DestoryEnemy ();
			Camera.main.GetComponent<Main> ().AddScore (enemySc);
		}
	}

//	private void OnTriggerEnter(Collider other)
//	{
//		if (lastContact== other.transform.root.gameObject)
//		{
//			return;
//		}
//
//		lastContact = other.transform.root.gameObject;
//		if (lastContact.tag=="Bullet")
//		{
//			Destroy(lastContact);//maybe not?
//			enemyHp--;
//		}
//	}

}

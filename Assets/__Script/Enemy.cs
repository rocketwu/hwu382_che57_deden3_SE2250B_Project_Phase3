using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public static List<GameObject> enemies;

    [Header("Set in Inspector")]
    public float speed;
	public int enemyHp;
	public int enemySc;
    public BoundsCheck boundsCheck;
    public GameObject powerUpPrefab;
    public float dropChance = 0.3f;
    public PowerUp.PowerUpType[] powerType = new PowerUp.PowerUpType[] { PowerUp.PowerUpType.AddBomb, PowerUp.PowerUpType.WhosYourDaddy };
	float time = 0f;
    public GameObject expPrefab;

    public abstract void Move();

    public void DestoryEnemy()
    {
        enemies.Remove(gameObject);
        Destroy(gameObject);
        Instantiate(expPrefab, transform.position, transform.rotation);
        if (powerUpPrefab != null&&Random.value <= dropChance)
        {
            dropPowerUp();
        }
    }

    private void dropPowerUp()
    {
        GameObject go = Instantiate(powerUpPrefab) as GameObject;
        PowerUp pu = go.GetComponent<PowerUp>();
        pu.type = powerType[Random.Range(0, powerType.Length)];
        go.transform.position = gameObject.transform.position;
    }

    protected void Awake()
    {
        boundsCheck = GetComponent<BoundsCheck>();
        //boundsCheck.keepOnScreen = false;
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

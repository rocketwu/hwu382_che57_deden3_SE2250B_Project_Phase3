﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {
	private Vector3 _shootingPosition;
	public GameObject bulletPrefab;
	public float bulletSpeed;
	public Main.weaponType wp = Main.weaponType.simpleWp;
    public delegate void FireDelegate();
    public FireDelegate fireDelegate;

    public GameObject bombPrefab;
    public Text bombTxt;
    private int _bombNum=0;
    public int bombNum
    {
        set
        {
            _bombNum=value;
            bombTxt.text = "Bombs: " + _bombNum;
        }
        get
        {
            return _bombNum;
        }
    }

    public int numOfRPGs;                                   //number of bombs
	public bool autoShot = false; // use it if you don't want to damage your space key when testing.
	private float counter = 0.1f;
	// Use this for initialization
	void Awake () {
		bulletSpeed = 50f;
        fireDelegate += fire;
        numOfRPGs = 2;
        bombNum = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.T))
			switchWeapon ();
		if (Input.GetKeyDown (KeyCode.Space))
			fire ();
        if (Input.GetKeyDown(KeyCode.B))
            bomb();
		if (Input.GetKeyDown (KeyCode.O)) {
			if (autoShot == false)
				autoShot = true;
			else
				autoShot = false;
		}
		if (autoShot == true) {
			counter += Time.deltaTime;
			if (counter >= 0.2f) {
				fire ();
				counter = 0f;
			}
		}
	}

    private void bomb()
    {
        if (bombNum > 0)
        {
            GameObject bomb = Instantiate<GameObject>(bombPrefab);
            bomb.transform.position = this.GetComponentInParent<Transform>().position;
            bombNum--;
        }
            
    }

	private void simpleShooting(){
//		if (Input.GetKeyDown (KeyCode.Space)) {
			_shootingPosition = this.GetComponentInParent<Transform> ().position;
			GameObject bullet = Instantiate<GameObject>(bulletPrefab);
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			bullet.transform.position = _shootingPosition;
			rb.velocity = Vector3.up * bulletSpeed;
//		}
	}

	private void blasterShooting(){
//		if (Input.GetKeyDown (KeyCode.Space)) {
			_shootingPosition = this.GetComponentInParent<Transform> ().position;
			for (float i = -1f; i <= 1f; i++) {
				GameObject bullet = Instantiate<GameObject>(bulletPrefab);
				Rigidbody rb = bullet.GetComponent<Rigidbody>();
				bullet.transform.position = _shootingPosition;
				bullet.transform.rotation = new Quaternion (0, 0, i * 0.6f, 0);
				//rb.velocity = Vector3.up * bulletSpeed;
				rb.velocity = new Vector3 (i*0.6f, 1f, 0) * bulletSpeed;
			}
//		}
	}

	private void switchWeapon(){
		if (wp == Main.weaponType.simpleWp)
			wp = Main.weaponType.blasterWp;
		else 
			wp = Main.weaponType.simpleWp;
	}

	private void fire(){
		if (wp == Main.weaponType.blasterWp)
			blasterShooting ();
		if (wp == Main.weaponType.simpleWp) {
			simpleShooting ();
		}
	}
}

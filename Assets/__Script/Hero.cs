using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    static public Hero S;
	public static Vector3 heroPosition;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
	//private weaponType _heroWp = weaponType.simpleWp;

    //for restrain=============
    //public float Bleeding = 1f;
    //private float camWidth;
    //private float camHeight;
    //for restrain=============

    [Header("Set Dynamically")]
    //public float shieldLevel = 1;
    private GameObject lastContact;
    private float _shieldLevel = 1;

    public float shieldLevel
    {
        get
        {
            return _shieldLevel;
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if(value<0)
            {
                Destroy(this.gameObject);
                Main.S.DelayedRestart();
            }
            if (value <= 0)
            {
                GetComponentInChildren<Shield>().gameObject.GetComponent<SphereCollider>().enabled = false;
                GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                GetComponentInChildren<Shield>().gameObject.GetComponent<SphereCollider>().enabled = true;
                GetComponent<SphereCollider>().enabled = false;
            }
        }
    }

    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.awake()-attempted to assign second Hero.S");
        }
        //for restrain=============
        //camHeight = Camera.main.orthographicSize;
        //camWidth = camHeight * Camera.main.aspect;
        //for restrain=============

}

	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += x * speed * Time.deltaTime;
        pos.y += y * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(y * pitchMult, x * rollMult, 0);
		heroPosition = transform.position;
        

		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if (lastContact== other.transform.root.gameObject)
        {
            return;
        }
        
        lastContact = other.transform.root.gameObject;
        if (lastContact.tag=="Enemy")
        {
            shieldLevel--;
            lastContact.GetComponent<Enemy>().DestoryEnemy();//maybe not?

        }else if(lastContact.tag == "ProjectileEnemy")
        {
            shieldLevel--;
            Destroy(lastContact);
        }


    }

}

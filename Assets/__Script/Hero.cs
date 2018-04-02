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
    public GameObject who;
    public Color whoColor;
	public float speedFactor = 1f;
    public GameObject expPrefab;
    //private weaponType _heroWp = weaponType.simpleWp;

    //for restrain=============
    //public float Bleeding = 1f;
    //private float camWidth;
    //private float camHeight;
    //for restrain=============

    [Header("Set Dynamically")]
    //public float shieldLevel = 1;
    private GameObject lastContact;
    private float _shieldLevel = 2;

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
                Instantiate(expPrefab, transform.position, transform.rotation);
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
        whoColor = who.GetComponent<Renderer>().material.color;
        who.GetComponent<Renderer>().material.color = new Color(0,0,0,0);

}


    // Update is called once per frame
    private float _whoTimer = 0f;
    
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
		pos.x += x * speed * Time.deltaTime * speedFactor;
		pos.y += y * speed * Time.deltaTime * speedFactor;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(y * pitchMult, x * rollMult, 0);
		heroPosition = transform.position;
		//-----------------------------------------------------------------
        if (_whosYourDaddy)
        {
            _whoTimer += Time.deltaTime;
            Color c = who.GetComponent<Renderer>().material.color;
            c.a = 1f - _whoTimer / 4f;
            who.GetComponent<Renderer>().material.color = c;
            if (_whoTimer >= 4f)
            {
                _whosYourDaddy = false;
                _whoTimer = 0f;
            }
        }
		//-----------------------------------------------------------------
		if (_isAutoFire)
		{
			_autoFireCounter += Time.deltaTime;
			if (_autoFireCounter >= 4f)
			{
				_isAutoFire = false;
				_autoFireCounter = 0f;
				GetComponentInChildren<Shooting> ().autoShot = false;
			}
		}
		//-----------------------------------------------------------------
		if (_isSpeedUp)
		{
			_speedUpCounter += Time.deltaTime;
			if (_speedUpCounter >= 4f)
			{
				_isSpeedUp = false;
				_speedUpCounter = 0f;
				speedFactor = 1f;
				this.GetComponentInChildren<MeshRenderer> ().material.color = Color.white;
			}
		}
		
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
            if (!_whosYourDaddy)
                shieldLevel--;
            lastContact.GetComponent<Enemy>().enemyHp-=10;//maybe not?

        }else if(lastContact.tag == "ProjectileEnemy")
        {
            if (!_whosYourDaddy)
                shieldLevel--;
            Destroy(lastContact);
        }else if (lastContact.tag == "PowerUp")
        {
            AbsorbPowerUp (lastContact);
        }


    }

    public void AbsorbPowerUp(GameObject lastContact)
    {
        PowerUp pu = lastContact.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case PowerUp.PowerUpType.AddBomb:
                GetComponentInChildren<Shooting>().bombNum++;
                break;
            case PowerUp.PowerUpType.WhosYourDaddy:
                whosYourDaddy();
                break;
			case PowerUp.PowerUpType.AutoShooting:
				autoFire();
				break;
			case PowerUp.PowerUpType.SpeedUp:
				speedUp();
				break;
        }
        pu.AbsorbedBy(this.gameObject);
    }
	//-----------------------------------------------------------------
    private bool _whosYourDaddy = false;
    public void whosYourDaddy()
    {
        who.GetComponent<Renderer>().material.color = whoColor;
        _whosYourDaddy = true;
        _whoTimer = 0f;
    }
	//-----------------------------------------------------------------
	private float _autoFireCounter = 0;
	private bool _isAutoFire = false;
	private void autoFire(){
		_isAutoFire = true;
		_autoFireCounter = 0;
		GetComponentInChildren<Shooting> ().autoShot = true;
	}
	//----------------------------------------------------------------
	private float _speedUpCounter = 0;
	private bool _isSpeedUp = false;
	private void speedUp(){
		_isSpeedUp = true;
		_speedUpCounter = 0;
		speedFactor = 3f;
		this.GetComponentInChildren<MeshRenderer> ().material.color = Color.blue;
	}

}

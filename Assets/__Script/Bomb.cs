using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private BoundsCheck bc;
    public float speed = 20f;
    private bool status = false;
    private float e = 1.1f;
    private GameObject lastContact;
    // Use this for initialization
    void Start () {
        bc = GetComponent<BoundsCheck>();
        GetComponent<Collider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!status)
        {
            fly();
        }
        else
        {
            explode();
        }
    }

    void fly()
    {
        Vector3 direction = -transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
        if (direction.magnitude < 0.5f) status = true;
    }
    void explode()
    {
        if (transform.localScale.x >= 100f)
        {
            Destroy(gameObject);
        }
        GetComponent<Collider>().enabled = true;
        transform.localScale += e*new Vector3(1,1,0);
        e += 0.1f;
        
    }
    private void OnTriggerEnter(Collider other)
    {

        lastContact = other.transform.root.gameObject;//.GetComponentInParent<GameObject>();
        if (lastContact.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<Enemy>().enemyHp-=100;
        }
        if (lastContact.tag == "ProjectileEnemy")
        {
            Destroy(lastContact);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [Header("Set in Inspector")]
    public float rotationSpeed = 0.1f;
    [Header("Set Dynamically")]
    public int levelShown=0;

    Material material;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
		
	}
	
	// Update is called once per frame
	void Update () {
        int currentLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        if (currentLevel!=levelShown)
        {
            levelShown = currentLevel;
            material.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        float z = -(rotationSpeed * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, z);
		
	}
}

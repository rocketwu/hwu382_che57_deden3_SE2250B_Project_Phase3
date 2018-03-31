using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTest : MonoBehaviour {
    public bool check = false;
    public Text cutsceneTxt;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (check)
        {
            changeTxt();
        }

    }
    public void changeTxt()
    {
        Color c = Random.ColorHSV();
        cutsceneTxt.color = c;
    }
}

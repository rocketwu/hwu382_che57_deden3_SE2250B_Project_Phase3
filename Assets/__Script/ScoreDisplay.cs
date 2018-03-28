using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	public Text scoreDisplay;
	public static int score;
	// Use this for initialization
	void Start () {
		score = 0;
		SetScoreDisplay ();
	}

//	void Update(){
//		SetScoreDisplay ();
//	}

	private void SetScoreDisplay()
	{
		scoreDisplay.text = "Score: " + score + "   High Score: "+ Main.highScore;

	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		
        if (Main.highScore <= score)
        {
            Main.highScore = score;
        }
        SetScoreDisplay();
    }
	// Update is called once per frame
	void Update () {
		
	}
}

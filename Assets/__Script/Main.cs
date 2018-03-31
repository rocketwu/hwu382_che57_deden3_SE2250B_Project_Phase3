using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    static public Main S;
	public static int highScore;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefultPadding = 1.5f;         //the default padding is used when the object dont have BoundsCheck script. this is good 
    public float restartDelay = 2f;
	public enum weaponType {simpleWp, blasterWp};
	//public weaponType wp = weaponType.simpleWp;
    private BoundsCheck boundsCheck;
	public Text highScoreText;

    //take from score display:===============================================================
    public Text scoreDisplay;
    public int score;

    private void Start()
    {
        score = 0;
        SetScoreDisplay();
    }
        private void SetScoreDisplay()
    {
        scoreDisplay.text = "Score: " + score + "   High Score: " + Main.highScore;

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
    //end of take from scoredisplay==========================================================

    private void Awake()
    {
        if (S!=null)
        {
            Debug.LogError("Main.awake()-attempted to assign second Main.S");
        }
        else
        {
            S = this;
        }
        readHigh();


        boundsCheck = GetComponent<BoundsCheck>();
        if (boundsCheck == null) Debug.LogError("no boundsCheck was found in Main");


        bool checkPrefab = true;
        foreach (GameObject ele in prefabEnemies){
            if (ele == null) checkPrefab = false;
        }
        if (prefabEnemies.Length < 1 || !checkPrefab) Debug.LogError("prefab attaching error");
        

        

        Invoke("SpawnEnemy", 0.5f / enemySpawnPerSecond);

    }

    public void DelayedRestart()
    {
        //if (highscore < scoredisplay.score)
        //      {
        //          highscore = scoredisplay.score;

        //      }
        setHigh();

			
		displayHighScore ();
        Invoke("Restart", restartDelay);
    }

    public void Restart()
    {
		highScoreText.text = "";
        SceneManager.LoadScene("GamePlay");
    }

    public void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[index]);

        float padding = enemyDefultPadding;
        if (go.GetComponent<BoundsCheck>() != null) padding = Mathf.Abs(go.GetComponent<BoundsCheck>().padding);

        Vector3 pos = Vector3.zero;
        pos.x = UnityEngine.Random.Range(-(boundsCheck.camWidth - padding), boundsCheck.camWidth - padding);
        pos.y = boundsCheck.camHeight + padding;

        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

    }

	public void displayHighScore(){
		highScoreText.text = "Your High Score is: " + highScore + "!";
	}

    private void readHigh()
    {
        try
        {
            string str = File.ReadAllText(@".\hs.txt");
            highScore = int.Parse(str);
        } catch(Exception ex)
        {
            highScore = 0;
            setHigh();
        }
    }

    private void setHigh()
    {
        try
        {
            File.WriteAllText(@".\hs.txt", highScore.ToString());
        }catch(Exception ex)
        {

        }
    }

}

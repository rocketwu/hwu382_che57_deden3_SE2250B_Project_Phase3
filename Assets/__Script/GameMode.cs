using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {

    [Header("Set in Inspector")]
    public Text levelText;
    public Text cutsceneTxt;
    public GameObject[] prefabEnemies;
    public GameObject[] prefabBosses;
    public float enemySpawnDeltaTime = 2f;
    public float enemyDefultPadding = 1.5f;
    private BoundsCheck boundsCheck;

    [Header("Set Dynamically")]
    public int threshold;
    public int level;
    public bool spawning = true;
    public Mode mode = Mode.Minion;
    public enum Mode {Minion, Boss, Cutscenes}

    private float timer = 0f;

	// Use this for initialization
	void Start () {
        level = 1;
        levelText.text = "Level: " + level;
        cutsceneTxt.enabled = false;
        threshold = 10;
        Enemy.enemies = new List<GameObject>();
        CheckPrefab();
        boundsCheck = GetComponent<BoundsCheck>();
        if (boundsCheck == null) Debug.LogError("no boundsCheck was found in GameMode");
    }
	
	// Update is called once per frame
	void Update () {
        ModeSwitcher();
        if (Hero.S.gameObject != null)
            switch (mode)
        {
            case Mode.Minion:
                timer += Time.deltaTime;
                if (timer >= enemySpawnDeltaTime && Main.S.score < threshold)
                {
                    SpawnEnemy();
                    timer = 0;
                }
                break;

            case Mode.Boss:
                if (spawning)
                {
                    spawning = false;
                    SpawnBoss();
                    
                }
                break;

            case Mode.Cutscenes:
                spawning = true;
                RunCutscenes();
                break;

            default:
                Debug.Log("Game mode error");
                break;
        }
	}

    private void LateUpdate()
    {
        
    }



    private void CheckPrefab()
    {
        bool checkPrefab = true;
        foreach (GameObject ele in prefabEnemies)
        {
            if (ele == null) checkPrefab = false;
        }
        foreach (GameObject ele in prefabBosses)
        {
            if (ele == null) checkPrefab = false;
        }
        if (prefabEnemies.Length < 1 || prefabBosses.Length < 1 || !checkPrefab) Debug.LogError("prefab attaching error");
    }

    private void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[index]);

        float padding = enemyDefultPadding;
        if (go.GetComponent<BoundsCheck>() != null) padding = Mathf.Abs(go.GetComponent<BoundsCheck>().padding);

        Vector3 pos = Vector3.zero;
        pos.x = UnityEngine.Random.Range(-(boundsCheck.camWidth - padding), boundsCheck.camWidth - padding);
        pos.y = boundsCheck.camHeight + padding;

        go.transform.position = pos;

        Enemy.enemies.Add(go);

        //set init score and hp
        Enemy temp = go.GetComponent<Enemy>();
        temp.enemyHp += (level-1) * 2;
        temp.enemySc += (level-1);
    }

    private void SpawnBoss()
    {
        int index = UnityEngine.Random.Range(0, prefabBosses.Length);
        GameObject go = Instantiate<GameObject>(prefabBosses[index]);

        go.transform.position = new Vector3(0, boundsCheck.camHeight - enemyDefultPadding*3, 0);

        Enemy.enemies.Add(go);
        //set init score and hp
        Enemy temp = go.GetComponent<Enemy>();
        temp.enemyHp *= (level);
        temp.enemySc *= (level);

    }

    private void ModeSwitcher()
    {
        if (mode != Mode.Cutscenes)
        {
            //not cutscene, check if there are more enemys
            if (Enemy.enemies.Count == 0 && (Main.S.score >= threshold || !spawning))
            {
                mode = Mode.Cutscenes;
            }
        }
        else
        {
            if (CutsceneDone())
            {
                level++;
                mode = (level % 2 == 0) ? Mode.Boss : Mode.Minion;
                threshold = Main.S.score + level * 20;
            }
        }

    }

    private bool CutsceneDone()
    {
        return !(cutsceneTxt.enabled);
    }

    private bool outOfScreen = false;
    private bool reseted;
    private void RunCutscenes()
    {
        if (!cutsceneTxt.enabled)
        {
            cutsceneTxt.enabled = true;
            Hero.S.enabled = false;
            Hero.S.gameObject.GetComponent<BoundsCheck>().keepOnScreen = false;
            Hero.S.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            outOfScreen = false;
            reseted = false;
        }
        else
        {
            
            changeTxt();
            
            outOfScreen = (outOfScreen || !(Hero.S.gameObject.GetComponent<BoundsCheck>().isOnScreen));
            if (!outOfScreen)
            {
                Hero.S.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.VelocityChange);
            }
            else
            {
                
                if (!reseted)
                {
                    reseted = true;
                    Hero.S.gameObject.transform.position = new Vector3(0, -(boundsCheck.camHeight) - 10, 0);
                }
                else if (Hero.S.gameObject.transform.position.y<=5f)
                {
                    
                    Hero.S.gameObject.transform.position+=new Vector3(0,3f*Time.deltaTime,0);
                }
                else
                {
                    cutsceneTxt.enabled = false;
                    Hero.S.enabled = true;
                    Hero.S.gameObject.GetComponent<BoundsCheck>().keepOnScreen = true;
                    Hero.S.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }

            }
        }
        

    }

    public void changeTxt()
    {

        cutsceneTxt.color = Random.ColorHSV();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    static public Main S;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefultPadding = 1.5f;         //the default padding is used when the object dont have BoundsCheck script. this is good 
    public float restartDelay = 2f;

    private BoundsCheck boundsCheck;

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


        boundsCheck = GetComponent<BoundsCheck>();
        if (boundsCheck == null) Debug.LogError("no boundsCheck was found in Main");


        bool checkPrefab = true;
        foreach (GameObject ele in prefabEnemies){
            if (ele == null) checkPrefab = false;
        }
        if (prefabEnemies.Length < 1 || !checkPrefab) Debug.LogError("prefab attaching error");
        

        

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

    }

    public void DelayedRestart()
    {
        Invoke("Restart", restartDelay);
    }

    public void Restart()
    {
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

    
}

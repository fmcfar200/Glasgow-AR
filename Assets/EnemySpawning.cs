using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    GameObject gameManager;


    public GameObject testPrefab;
    int spawnAmount;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        if (gameManager == null)
        {
            Debug.LogError("Cannot find Game Manager");
        }
    }
	// Use this for initialization
	void Start ()
    {
        spawnAmount = gameManager.GetComponent<GameManagerScript>().enemyAmount -1;

        if (spawnAmount > 0)
        {
            for(int i = 0; i < spawnAmount; i++)
            {
                Vector3 randomPos = Random.insideUnitSphere * 6;
                randomPos.y = 0.25f;
                Instantiate(testPrefab, randomPos, Quaternion.identity);
                
            }
            spawnAmount = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    GameObject gameManager;
    List<EnemyIcon.Type> theTypes;

    public List<GameObject> enemies = new List<GameObject>();
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
        spawnAmount = gameManager.GetComponent<GameManagerScript>().enemyAmount;
        theTypes = gameManager.GetComponent<GameManagerScript>().types;

        if (spawnAmount > 0 && theTypes.Count > 0)
        {
            for(int i = 0; i < spawnAmount; i++)
            {
                Vector3 randomPos = Random.insideUnitSphere * 6;
                randomPos.y = 0.25f;

                foreach(EnemyIcon.Type type in theTypes)
                {
                    if (type == EnemyIcon.Type.BAT)
                    {
                        Instantiate(enemies[0], randomPos, Quaternion.identity);

                    }
                    else if (type == EnemyIcon.Type.SKELETON)
                    {
                        Instantiate(enemies[1], randomPos, Quaternion.identity);

                    }
                }
                
            }
            spawnAmount = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

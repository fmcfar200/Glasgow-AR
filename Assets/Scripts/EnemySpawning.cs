using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

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

	void Start ()
    {
        spawnAmount = gameManager.GetComponent<GameManagerScript>().enemyAmount;
        theTypes = gameManager.GetComponent<GameManagerScript>().types;

        if (spawnAmount > 0 && theTypes.Count > 0)
        {

                foreach (EnemyIcon.Type type in theTypes)
                {
                    
                   Vector3 randomPos = Random.insideUnitSphere * 6;
                   randomPos.y = 0.25f;

                   if (type == EnemyIcon.Type.BAT)
                     {
                        Instantiate(enemies[0], randomPos, Quaternion.identity);

                     }

                   if (type == EnemyIcon.Type.SKELETON)
                     {
                            Instantiate(enemies[1], randomPos, Quaternion.identity);

                     }

                spawnAmount--;
      
                }

            spawnAmount = 0;
        }
	}
}

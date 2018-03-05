using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    GameObject gameManager;
    List<EnemyIcon.Type> theTypes;
    List<int> theLevels;

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
        theLevels = gameManager.GetComponent<GameManagerScript>().levels;

        if (spawnAmount > 0 && theTypes.Count > 0)
        {
            List<GameObject> spawnedObjects = new List<GameObject>();

                foreach (EnemyIcon.Type type in theTypes)
                {
                   GameObject thisEnemy;
                   Vector3 randomPos = Random.insideUnitSphere * 6;
                   randomPos.y = 0.25f;

                   if (type == EnemyIcon.Type.BAT)
                     {
                        thisEnemy = Instantiate(enemies[0], randomPos, Quaternion.identity);
                    spawnedObjects.Add(thisEnemy);
                     }

                   if (type == EnemyIcon.Type.SKELETON)
                     {
                        thisEnemy = Instantiate(enemies[1], randomPos, Quaternion.identity);
                        spawnedObjects.Add(thisEnemy);
                     }

                spawnAmount--;
                thisEnemy = null;
      
                }

                for(int i = 0; i < spawnedObjects.Count;i++)
                {
                Debug.Log(theLevels[i] + " ");
                    spawnedObjects[i].transform.GetChild(0).GetComponent<EnemyInfo>().level = theLevels[i];
                }
                
            spawnAmount = 0;
            spawnedObjects.Clear();
           
        }
	}
}

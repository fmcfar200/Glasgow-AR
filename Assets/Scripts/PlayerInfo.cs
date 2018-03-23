using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public int maxHealth;
    public int maxMana;
    private int maxXP;


    public int currentHealth;
    public int currentMana;

    public int currentXP;
    public int currentLevel;

    GameObject gameManagerObj;


	// Use this for initialization
	void Start ()
    {
        gameManagerObj = GameObject.FindGameObjectWithTag("Game Manager");
        if (gameManagerObj != null)
        {
            currentLevel = gameManagerObj.GetComponent<GameManagerScript>().playerLevel;
            currentXP = gameManagerObj.GetComponent<GameManagerScript>().currentXP;

        }
        else
        {
            Debug.LogError("Cant find game manager");
        }

        maxXP = currentLevel * 100;

        maxHealth = 100 + (25*currentLevel);
        maxMana = 100 + (20*currentLevel);

        currentHealth = maxHealth;
        currentMana = maxMana;
        	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (currentXP >= maxXP)
        {
            LevelUp();

        }
        else if (currentHealth <= 0)
        {
            Die();
        }
	}


    public void GiveXP(int amount)
    {
        
        currentXP += amount;
        
    }

    public void SaveLevel()
    {
        gameManagerObj.GetComponent<GameManagerScript>().playerLevel = currentLevel;
        gameManagerObj.GetComponent<GameManagerScript>().currentXP = currentXP;

        gameManagerObj.GetComponent<GameManagerScript>().SaveData();

    }

    public void LevelUp()
    {
        currentLevel++;
        maxXP = currentLevel * 100;
        SaveLevel();
    }

    void Die()
    {
        if (currentLevel > 1)
        {
            currentLevel = 1;
            currentXP = 0;
            SaveLevel();

            gameManagerObj.GetComponent<GameManagerScript>().ReloadScene();

        }
    }
}

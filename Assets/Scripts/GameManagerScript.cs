﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript manager;

    public GameObject testPrefab;

    public int enemyAmount;
    public List<EnemyIcon.Type> types = new List<EnemyIcon.Type>();

    Scene theScene;

	void Start()
	{
        theScene = SceneManager.GetActiveScene();

        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
        
	}

    void Update()
    {
        theScene = SceneManager.GetActiveScene();
        switch (theScene.name)
        {
            case "AR Scene":
                Camera.main.GetComponent<VuforiaBehaviour>().enabled = true;

                break;
            case "Map Scene":
                Camera.main.GetComponent<VuforiaBehaviour>().enabled = false;

                break;

        }

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


}

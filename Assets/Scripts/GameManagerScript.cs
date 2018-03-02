using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript manager;
    public GameObject testPrefab;

    public int enemyAmount;
    public List<EnemyIcon.Type> types = new List<EnemyIcon.Type>();

    Scene scene;

	void Start()
	{

        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

		scene = SceneManager.GetActiveScene ();

		if (scene.name == "AR Scene") {

			Camera.main.GetComponent<VuforiaBehaviour> ().enabled = true;
        
		} 
		else 
		{
			Camera.main.GetComponent<VuforiaBehaviour> ().enabled = false;
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

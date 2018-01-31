using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GameManagerScript : MonoBehaviour {


	void Start()
	{
		Scene scene = SceneManager.GetActiveScene ();

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

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}

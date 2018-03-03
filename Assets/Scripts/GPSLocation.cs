using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSLocation : MonoBehaviour {

    public float lat, lon;
    public Text infoText;


	// Use this for initialization
	void Start ()
    {
        //starts test on init
        //StartCoroutine(TestLocServices());

        Input.location.Start();


	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.location.status == LocationServiceStatus.Running)
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;

        }

        infoText.text = "Location: " + lat + "  " + lon;

    }

    /*
    void OnTriggerExit(Collider coll)
    {
        
        if (coll.gameObject.tag == "Enemy Icon")
        {
            Debug.Log("Enemy Icon");
            if (mapManager != null)
            {
                mapManager.ClearEnemies();
                mapManager.SpawnEnemyIcons();
            }
        }
    }
    */

    IEnumerator TestLocServices()
    {
        //is service enabled?
        if (!Input.location.isEnabledByUser)
            yield break;
       
        //Start Service
        Input.location.Start();

        //wait for init
        int maxWait = 15;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //timed out
        if (maxWait < 1)
        {
            Debug.LogError("Location Services Timed Out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Location Service Failed");
            yield break;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Vuforia;

using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;


public class MapManager : MonoBehaviour {

    public AbstractMap map;
    public GameObject player;

    private float pLat, pLon;

    public int enemyAmount = 0;
    private GameObject gameManager;



	// Use this for initialization
	void Start ()
    {

        player = GameObject.FindGameObjectWithTag("PlayerIcon");
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        if (player == null)
        {
            Debug.LogError("Player Object not found!");
        }

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "AR Scene")
        {

            Camera.main.GetComponent<VuforiaBehaviour>().enabled = true;

        }
        else
        {
            Camera.main.GetComponent<VuforiaBehaviour>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
      if (player != null && map != null)
      {
          pLat = player.GetComponent<GPSLocation>().lat;
          pLon = player.GetComponent<GPSLocation>().lon;
          Vector2d enemyLatLon = new Vector2d((double)pLat, (double)pLon);
          if (Input.GetMouseButtonDown(0)
              || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
          {
                Debug.Log("click");
                Vector3 enemyIconPos = Conversions.GeoToWorldPosition(enemyLatLon, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
                
                float randomX, randomZ;
                randomX = Random.Range(-2.6f, 2.6f);
                randomZ = Random.Range(-2.6f, 2.6f);
                enemyIconPos.x += randomX;
                enemyIconPos.z += randomZ;
                
                GameObject testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                testObject.transform.position = enemyIconPos;
                
                enemyAmount++;
                gameManager.GetComponent<GameManagerScript>().enemyAmount = enemyAmount;
          
          }
      }

     }
 }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Vuforia;

using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;



public class MapManager : MonoBehaviour {

    public AbstractMap map;
    public GameObject player;

    private float pLat, pLon;

    public int enemyAmount = 0;
    private GameObject gameManager;

    ILocationProvider _locationProvider;
    ILocationProvider LocationProvider
    {
        get
        {
            if (_locationProvider == null)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
    }

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

        LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;

    }
    void LocationProvider_OnLocationUpdated(object sender, LocationUpdatedEventArgs e)
    {
        LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
        SpawnEnemyIcons();
        
    }

    // Update is called once per frame
    void Update()
    {
        
      if (player != null && map != null)
      {
          pLat = player.GetComponent<GPSLocation>().lat;
          pLon = player.GetComponent<GPSLocation>().lon;
          
      }

     }

    void SpawnEnemyIcons()
    {
        Debug.Log("Location Update");

        Vector2d enemyLatLon = new Vector2d((double)pLat, (double)pLon);
        int spawnAmount = Random.Range(1, 4);

        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 enemyIconPos = Conversions.GeoToWorldPosition(enemyLatLon, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();

            float randomX, randomZ;
            randomX = Random.Range(-2.6f, 2.6f);
            randomZ = Random.Range(-2.6f, 2.6f);
            enemyIconPos.x += randomX;
            enemyIconPos.z += randomZ;

            GameObject testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            testObject.tag = "Enemy Icon";
            testObject.transform.position = enemyIconPos;

            enemyAmount++;
        }

        
        gameManager.GetComponent<GameManagerScript>().enemyAmount = enemyAmount;

    }
    
 }


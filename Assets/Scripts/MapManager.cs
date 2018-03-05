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

    private GameObject gameManager;
    public AbstractMap map;
    public GameObject player;

    private float pLat, pLon;
    float lastLat;
    float lastLon;

    public int enemyAmount = 0;
    public bool moved = false;

    public List<GameObject> enemyIcons = new List<GameObject>();


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


    }
 
    // Update is called once per frame
    void Update()
    {
        
      if (player != null && map != null)
      {
          pLat = player.GetComponent<GPSLocation>().lat;
          pLon = player.GetComponent<GPSLocation>().lon;

            if (pLat != lastLat && pLon != lastLon )
            {
                ClearEnemies();
                SpawnEnemyIcons();
            }

          lastLat = player.GetComponent<GPSLocation>().lat;
          lastLon = player.GetComponent<GPSLocation>().lon;

        }

      if (Input.GetMouseButtonDown(1))
        {
            ClearEnemies();
            SpawnEnemyIcons();
        }

   
     }


    public void SpawnEnemyIcons()
    {
        Debug.Log("Location Update");
       

        Vector2d enemyLatLon = new Vector2d((double)pLat, (double)pLon);
        int spawnAmount = Random.Range(2, 6);

        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 enemyIconPos = Conversions.GeoToWorldPosition(enemyLatLon, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();

            float randomX, randomZ;
            randomX = Random.Range(-2.6f, 2.6f);
            randomZ = Random.Range(-2.6f, 2.6f);
            enemyIconPos.x += randomX;
            enemyIconPos.z += randomZ;

            int randomIndex = Random.Range(0, enemyIcons.Count);
            GameObject icon = GameObject.Instantiate(enemyIcons[randomIndex], enemyIconPos, Quaternion.identity);

            int playerLevel = player.GetComponent<PlayerInfo>().currentLevel;
            int enemyLevel;
            if (playerLevel > 1)
            {
                 enemyLevel = Random.Range(playerLevel - 1, playerLevel + 2);

            }
            else
            {
                 enemyLevel = Random.Range(playerLevel, playerLevel + 2);

            }

            icon.GetComponent<EnemyIcon>().level = enemyLevel;
            icon.tag = "Enemy Icon";
            enemyAmount++;
        }

        UpdateManager();

    }

    

    public void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy Icon");

        foreach (GameObject e in enemies)
        {
            Destroy(e.gameObject);
        }

        enemies = null;
        enemyAmount = 0;
    }

    private void UpdateManager()
    {
        gameManager.GetComponent<GameManagerScript>().enemyAmount = enemyAmount;


        gameManager.GetComponent<GameManagerScript>().types.Clear();
        gameManager.GetComponent<GameManagerScript>().levels.Clear();

        GameObject[] icons = GameObject.FindGameObjectsWithTag("Enemy Icon");
        foreach(GameObject icon in icons)
        {
            gameManager.GetComponent<GameManagerScript>().types.Add(icon.GetComponent<EnemyIcon>().theType);
            gameManager.GetComponent<GameManagerScript>().levels.Add(icon.GetComponent<EnemyIcon>().level);
        }
    }

}


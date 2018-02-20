using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;


public class MapManager : MonoBehaviour {


    public AbstractMap map;
    private GameObject player;

    private float pLat, pLon;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("PlayerIcon");

        if (player == null)
        {
            Debug.LogError("Player Object not found!");
        }
	}
	
	// Update is called once per frame
	void Update ()
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
                GameObject testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                testObject.transform.position = enemyIconPos;
            }
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;

public class MarkerIconScript : MonoBehaviour {

    public AbstractMap map;

    public double Lat;
    public double Lon;

    Vector2d enemyLatLon;
    Vector3 enemyIconPos;

    void Update()
    {
        enemyLatLon = new Vector2d((double)Lat, (double)Lon);
        enemyIconPos = Conversions.GeoToWorldPosition(enemyLatLon, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
        transform.position = enemyIconPos;

    }

}

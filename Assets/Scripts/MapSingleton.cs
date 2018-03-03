using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSingleton : MonoBehaviour
{

    public static GameObject map;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (map == null)
        {
            map = this.gameObject;
        }
        else
        {
            DestroyObject(this.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIcon : MonoBehaviour {

    public int level = 1;

    public enum Type
    {
        BAT,
        SKELETON,
        GHOST,
        SLIME

    }

    public Type theType;

  
    public Type GetType()
    {
        return theType;
    }

    void LookAtCamera()
    {
        Vector3 lookPosition = Camera.main.transform.position - transform.position;
        lookPosition.z = 0;
        Quaternion theRotation = Quaternion.LookRotation(lookPosition);
    }
}

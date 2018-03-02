using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour {

    public enum Type
    {
        BAT,
        SKELETON,

    }

    public Type theType;


    public Type GetType()
    {
        return theType;
    }
}

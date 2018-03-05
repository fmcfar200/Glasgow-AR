using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour {

    public int level = 1;
    public TextMesh levelText;

    public enum Type
    {
        BAT,
        SKELETON,

    }

    public Type theType;


    void Start()
    {
        levelText.text = "Lvl: " + level.ToString();

    }



    public Type GetType()
    {
        return theType;
    }
}

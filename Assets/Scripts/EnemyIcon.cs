using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Update()
    {
        LookAtCamera();
    }

    public Type GetType()
    {
        return theType;
    }

    void LookAtCamera()
    {
        Vector3 lookPosition = Camera.main.transform.position - transform.position;
        lookPosition.z = 0;
        Quaternion theRotation = Quaternion.LookRotation(lookPosition);
        levelText.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, theRotation, 1.0f * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    private int maxHealth;
    private int maxMana;

    public int currentHealth;
    public int currentMana;

	// Use this for initialization
	void Start ()
    {
        maxHealth = 100;
        maxMana = 100;

        currentHealth = maxHealth;
        currentMana = maxMana;
        	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

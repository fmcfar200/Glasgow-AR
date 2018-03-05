using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour {

    public int level;

    public int currentHealth;
    int maxHealth;

    public TextMesh levelText;
    public Image healthBar;

    // Use this for initialization
    void Start ()
    {
        level = 1;
        maxHealth = 75 + (25 * level);
        currentHealth = maxHealth;
        levelText.text = "Lvl: " + level.ToString();

    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(currentHealth);
        float healthBarFlt = (float)currentHealth/(float)maxHealth;
        healthBar.fillAmount = healthBarFlt;
	}
}

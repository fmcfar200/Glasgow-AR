using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int currentHealth;
    int maxHealth = 100;
    bool alive = true;

    Animator animator;

    GameObject player;
    PlayerCombatScript playerCombat;

    Shader defaultShader, targetShader;

    

	void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        defaultShader = Shader.Find("Mobile/Diffuse");
        targetShader = Shader.Find("Outlined/Silhouetted Diffuse");

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerCombat = player.GetComponent<PlayerCombatScript>();
        }
        else
        {
            Debug.LogError("ERROR: PLAYER NOT FOUND");
        }
    }

    void Update()
    {
        LookAtCamera();


        if (currentHealth <= 0)
        {
            Death();
        }

        GameObject child = this.transform.GetChild(2).gameObject;
        if (this.gameObject == playerCombat.GetTarget())
        {
            child.GetComponent<Renderer>().material.shader = targetShader;
        }
        else
        {
            child.GetComponent<Renderer>().material.shader = defaultShader;

        }

    }

    void Death()
    {
        
        animator.SetInteger("Dead", 1);
        alive = false;

        if (this.gameObject == playerCombat.GetTarget())
        {
            playerCombat.ClearTarget();
        }
        StartCoroutine(WaitandDestroy(5.0f));

    }

    IEnumerator WaitandDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);

    }

    void LookAtCamera()
    {
        Vector3 lookPosition = Camera.main.transform.position - transform.position;
        lookPosition.y = 0;
        Quaternion theRotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, theRotation, 1.0f * Time.deltaTime);
    }
}

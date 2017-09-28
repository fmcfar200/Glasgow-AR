using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int currentHealth;
    int maxHealth = 100;
    bool alive = true;

    Animator animator;

	void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
        
    }

    void Death()
    {
        animator.SetInteger("Dead", 1);
        alive = false;
    }

    IEnumerator WaitandDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);

    }
}

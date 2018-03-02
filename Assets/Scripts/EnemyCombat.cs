using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

    public int currentHealth;
    int maxHealth = 100;
    bool alive = true;
    bool hit = false;


    Animator animator;

    GameObject player;
    PlayerCombatScript playerCombat;
    PlayerInfo playerInfo;

    bool playerTurn;

    Shader defaultShader, targetShader;


    enum State
    {
        IDLE,
        ATTACKING,
        STUNNED
    }
    State currentState;


    void Start()
    {
        currentState = State.IDLE;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        defaultShader = Shader.Find("Mobile/Diffuse");
        targetShader = Shader.Find("Outlined/Diffuse");

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerCombat = player.GetComponent<PlayerCombatScript>();
            playerInfo = player.GetComponent<PlayerInfo>();

        }
        else
        {
            Debug.LogError("ERROR: PLAYER NOT FOUND");
        }
    }

    void Update()
    {
        LookAtCamera();
        UpdateShader();

        playerTurn = playerCombat.myTurn;

        if (currentHealth <= 0)
        {
            Death();
        }
        else if (this.gameObject == playerCombat.GetTarget() && playerTurn == false && currentState == State.IDLE)
        {
            currentState = State.ATTACKING;
        }

        switch(currentState)
        {
            case State.IDLE:
                hit = false;
                animator.SetInteger("Attack", 0);
                animator.SetInteger("Idle", 1);
                break;
            case State.ATTACKING:
                Attack();
                break;
            case State.STUNNED:
                break;
        }

       
    }

    void Attack()
    {
        animator.SetInteger("Idle", 0);
        animator.SetInteger("Attack", 1);

        int randomAmount = Random.Range(18, 28);

        if (!hit)
        {
            StartCoroutine(WaitandEndTurn(animator.GetCurrentAnimatorStateInfo(0).length - 0.5f));
            playerInfo.currentHealth -= randomAmount;
            hit = true;

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
        StartCoroutine(WaitandDestroy(3.0f));

    }

    IEnumerator WaitandDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);

    }
    IEnumerator WaitandEndTurn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentState = State.IDLE;
        playerTurn = true;
        playerCombat.myTurn = playerTurn;
    }

    void LookAtCamera()
    {
        Vector3 lookPosition = Camera.main.transform.position - transform.position;
        lookPosition.y = 0;
        Quaternion theRotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, theRotation, 1.0f * Time.deltaTime);
    }

    void UpdateShader()
    {
        GameObject child;
        if (this.gameObject.name == "Bat_Red")
        {
            child = this.transform.Find("Mesh").gameObject;
        }
        else
        {
             child = this.transform.GetChild(2).gameObject;

        }


        if (this.gameObject == playerCombat.GetTarget())
        {
            child.GetComponent<Renderer>().material.shader = targetShader;
        }
        else
        {
            child.GetComponent<Renderer>().material.shader = defaultShader;

        }
    }
}

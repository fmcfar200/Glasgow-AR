using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

    
    bool alive = true;
    bool hit = false;

    public bool poisoned = false;
    int poisonTurns = 3;
    private int poisonDamage = 40;
    EnemyInfo enemyInfo;
    Animator animator;

    GameObject player;
    PlayerCombatScript playerCombat;
    PlayerInfo playerInfo;

    bool playerTurn;
    bool giveXP = false;

    Shader defaultShader, targetShader;


    public enum State
    {
        IDLE,
        ATTACKING,
        STUNNED,
        POISONED
    }
    public State currentState;


    void Start()
    {
        currentState = State.IDLE;
        animator = GetComponent<Animator>();
        enemyInfo = GetComponent<EnemyInfo>();

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



        if (enemyInfo.currentHealth <= 0)
        {
            Death();
        }
        if (this.gameObject == playerCombat.GetTarget() && playerTurn == false)
        {
            if (currentState == State.IDLE)
            {
                currentState = State.ATTACKING;
            }
            else if (currentState == State.STUNNED)
            {
                currentState = State.ATTACKING;
            }
        }
        
       if (poisoned)
        {
            enemyInfo.healthBar.color = Color.green;
        }
       else
        {
            enemyInfo.healthBar.color = Color.white;

        }

        switch (currentState)
        {
            case State.IDLE:
                hit = false;
                animator.SetInteger("Attack", 0);
                animator.SetInteger("Stunned", 0);
                animator.SetInteger("Idle", 1);
                break;
            case State.ATTACKING:
                
                    Attack();
                
                break;
            case State.STUNNED:
                animator.SetInteger("Idle", 0);
                animator.SetInteger("Stunned", 1);
                break;

        }

       
    }

    void Attack()
    {
        animator.SetInteger("Idle", 0);
        animator.SetInteger("Stunned", 0);
        animator.SetInteger("Attack", 1);
        
        int randomAmount = Random.Range(18, 28) + (enemyInfo.level);

        if (!hit)
        {
            if (poisoned)
            {
                enemyInfo.currentHealth -= poisonDamage;
                poisonTurns--;
                if (poisonTurns <= 0)
                {
                    poisoned = false;
                }

            }

            if (this.gameObject.name == "Infernal_Dog")
            {
                StartCoroutine(WaitandEndTurn(animator.GetCurrentAnimatorStateInfo(0).length - 24f));

            }
            else
            {
                StartCoroutine(WaitandEndTurn(animator.GetCurrentAnimatorStateInfo(0).length - 0.5f));

            }
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

        if (!giveXP)
        {
            giveXP = true;
            playerInfo.GiveXP(enemyInfo.level * 100 / 2);

        }
        playerInfo.SaveLevel();

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
        if (this.gameObject.name == "Bat_Red" || this.gameObject.name == "Ghost_Green" || this.gameObject.name == "Slime_Blue" ||this.gameObject.name == "Warrior" || this.gameObject.name == "Infernal_Dog")
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

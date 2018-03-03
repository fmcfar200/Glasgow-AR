﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour {

    PlayerInfo playerInfo;

    RaycastHit hit;
    Vector2[] touches = new Vector2[5];

    public bool myTurn;
    private bool giveMana = false;
    private GameObject target;



    public bool fire, stun;
    private float fireSpeed = 6.0f;

    public GameObject fireballPrefab;
    public GameObject stunEffectPrefab;

    private GameObject theFireball;
    private GameObject theStunEffect;

    private List<GameObject> fireBallList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        playerInfo = GetComponent<PlayerInfo>();
        myTurn = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.touchCount > 0)
        {
            foreach(Touch t in Input.touches)
            {
                touches[t.fingerId] = Camera.main.ScreenToWorldPoint(Input.GetTouch(t.fingerId).position);
                if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(
                        Input.GetTouch(t.fingerId).position);
                    if (Physics.Raycast(ray,out hit))
                    {
                        if (hit.collider.gameObject.tag == "enemy")
                        {

                            target = hit.collider.gameObject;
                        }
                    }
                }
                   
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray,out hit))
                {
                    if (hit.collider.gameObject.tag == "enemy")
                    {
                        target = hit.collider.gameObject;

                    }
                }
      
            }
        }

        if (target != null && myTurn)
        {
            if (giveMana)
            {
                giveMana = false;
                playerInfo.currentMana += Random.Range(10, 20);
            }


            if (fire && playerInfo.currentMana >= 30 &&theFireball == null)
            {
                fire = false;

                playerInfo.currentMana -= 30;
                Vector3 fireballPosition = Camera.main.transform.position;
                fireballPosition.x = fireballPosition.x - 1.0f;
                theFireball = (GameObject)Instantiate(fireballPrefab, fireballPosition, Quaternion.identity) as GameObject;
                fireBallList.Add(theFireball);

            }

            else if (theFireball != null)
            {
                float step = fireSpeed * Time.deltaTime;
                theFireball.transform.position = Vector3.MoveTowards(theFireball.transform.position, target.transform.position, step);

                if (theFireball.transform.position == target.transform.position)
                {
                    target.GetComponent<EnemyCombat>().currentHealth -= 25;
                    Destroy(theFireball);

                    StartCoroutine(WaitAndEndTurn(0.5f));
                }
            }

            if (stun && playerInfo.currentMana >= 40 && theStunEffect == null)
            {
                stun = false;

                playerInfo.currentMana -= 40;
                Vector3 stunEffectPosition = target.transform.position;
                theStunEffect = (GameObject)Instantiate(stunEffectPrefab, stunEffectPosition, Quaternion.identity) as GameObject;
    
            }
            else if (theStunEffect != null)
            {
                StartCoroutine(WaitAndDestroy(theStunEffect.GetComponent<ParticleSystem>().main.duration, theStunEffect));
                target.GetComponent<EnemyCombat>().currentState = EnemyCombat.State.STUNNED;
                StartCoroutine(WaitAndNewTurn(0.5f));

            }


        }
        else if (target == null)
        {
            foreach(GameObject fb in fireBallList)
            {
                Destroy(fb);
            }
            fireBallList.Clear();
            myTurn = true;
        }
        
        

    }

    

    public GameObject GetTarget()
    {
        return target;
    }

    public void ClearTarget()
    {
        target = null;
    }
    IEnumerator WaitAndNewTurn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        theFireball = null;
        theStunEffect = null;

        giveMana = true;
        myTurn = true;
    }
    IEnumerator WaitAndEndTurn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        theFireball = null;
        giveMana = true;
        myTurn = false;
    }

    IEnumerator WaitAndDestroy(float seconds, GameObject theObject)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(theObject);
    }


}

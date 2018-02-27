using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour {

    RaycastHit hit;
    Vector2[] touches = new Vector2[5];

    public bool myTurn;
    private GameObject target;
    public bool fire;
    private float fireSpeed = 6.0f;

    public GameObject fireballPrefab;
    private GameObject theFireball;
    private List<GameObject> fireBallList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
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
            if (fire && theFireball == null)
            {
                fire = false;
                Vector3 fireballPosition = Camera.main.transform.position;
                fireballPosition.x = fireballPosition.x - 1.0f;
                theFireball = (GameObject)Instantiate(fireballPrefab, fireballPosition, Quaternion.identity) as GameObject;
                fireBallList.Add(theFireball);

            }

            if (theFireball != null)
            {
                float step = fireSpeed * Time.deltaTime;
                theFireball.transform.position = Vector3.MoveTowards(theFireball.transform.position, target.transform.position, step);

                if (theFireball.transform.position == target.transform.position)
                {
                    target.GetComponent<Enemy>().currentHealth -= 25;
                    Destroy(theFireball);
                    theFireball = null;
                    myTurn = false;
                }
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour {

    RaycastHit hit;
    Vector2[] touches = new Vector2[5];
	// Use this for initialization
	void Start () {
		
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

                            hit.collider.GetComponent<Enemy>().currentHealth -= 25;
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
                        print("hit");
                        hit.collider.GetComponent<Enemy>().currentHealth -= 25;
                    }
                }
                    

                
            }
        }
	}
}

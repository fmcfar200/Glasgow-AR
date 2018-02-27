using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonScript : MonoBehaviour {

    PlayerCombatScript playerCombat;
    PlayerInfo playerInfo;

    public Button[] buttons;

    public Image playerHealthBar;
    public Image manaBar;

    Scene scene;

    void Start()
    {
         scene = SceneManager.GetActiveScene();

        if (scene.name == "AR Scene")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerCombat = player.GetComponent<PlayerCombatScript>();
            playerInfo = player.GetComponent<PlayerInfo>();
            if (playerCombat == null)
            {
                Debug.LogError("Player combat not found");
            }
        }

       
    }

	public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void FireButton()
    {
        playerCombat.fire = true;
    }


    void Update()
    {
        if (scene.name == "AR Scene")
        {
            foreach (Button bt in buttons)
            {
                if (playerCombat.GetTarget() == null ||playerCombat.myTurn == false)
                {
                    bt.interactable = false;

                }
                else
                {
                    bt.interactable = true;

                }
            }

            if (playerCombat.myTurn == true)
            {
                playerHealthBar.fillAmount = (float)playerInfo.currentHealth / 100;
                manaBar.fillAmount = (float)playerInfo.currentMana / 100;
            }
           
        }
    }
}

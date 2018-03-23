using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    PlayerCombatScript playerCombat;
    PlayerInfo playerInfo;

    public Button[] buttons;

    public Image playerHealthBar;
    public Image manaBar;

    public Text playerLevelText;

    Scene scene;

    GameObject DevMenu;
    bool openMenu = false;

    AudioManager audioManager;

    void Start()
    {
         scene = SceneManager.GetActiveScene();
         audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
        else
        {
            if (scene.name == "Map Scene")
            {
                DevMenu = GameObject.FindGameObjectWithTag("DMenu");
                DevMenu.SetActive(false);
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
    public void StunButton()
    {
        playerCombat.stun = true;
    }
    public void Poison()
    {
        playerCombat.poison = true;
    }

    public void EndTurn()
    {
        playerCombat.endTurn = true;
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
                playerHealthBar.fillAmount = (float)playerInfo.currentHealth / (float)playerInfo.maxHealth;
                manaBar.fillAmount = (float)playerInfo.currentMana / (float)playerInfo.maxMana;
            }

            CheckAvaiable();

            playerLevelText.text = playerInfo.currentLevel.ToString();
           
        }
        else if (scene.name == "Map Scene")
        {
            playerLevelText.text = GameObject.FindGameObjectWithTag("PlayerIcon").GetComponent<PlayerInfo>().currentLevel.ToString();

            /*
            if (Input.touchCount > 0)
            {
                foreach (Touch t in Input.touches)
                {
                    if (Input.GetTouch(t.fingerId).phase == TouchPhase.Stationary)
                    {
                        openMenu = true;
                    }
                    else
                    {
                        openMenu = false;
                    }

                }
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                openMenu = true;
            }
            */

            if (openMenu)
            {
                DevMenu.gameObject.SetActive(true);
            }
            else
            {
                DevMenu.gameObject.SetActive(false);
            }
        }
    }

    public void setMenuopen()
    {
        if (openMenu)
        {
            openMenu = false;
        }
        else
        {
            openMenu = true;
        }
    }

   

    void CheckAvaiable()
    {
        if (playerInfo.currentMana >= playerCombat.fireCost)
        {
            buttons[0].interactable = true;
        }
        else
        {
            buttons[0].interactable = false;
        }

        if (playerInfo.currentMana >= playerCombat.stunCost)
        {
            buttons[1].interactable = true;

        }
        else
        {
            buttons[1].interactable = false;

        }

        if (playerInfo.currentMana >= playerCombat.poisonCost)
        {
            buttons[2].interactable = true;

        }
        else
        {
            buttons[2].interactable = false;

        }

    }
}

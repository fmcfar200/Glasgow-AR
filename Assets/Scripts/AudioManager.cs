using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {


    
    public AudioClip ButtonClick;
    public AudioClip LevelUp;
    public AudioClip TargetSelect;
    public AudioClip winSound;

    public List<AudioClip> fightSounds = new List<AudioClip>();
    public List<AudioClip> music = new List<AudioClip>();

    AudioSource musicSource;

    Scene scene;

    GameObject player;

    bool hasSwitched = false;
       
	// Use this for initialization
	void Start ()
    {
        musicSource = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();

        musicSource.clip = music[0];
        musicSource.PlayDelayed(0);

        if (scene.name == "AR Scene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            player = null;
        }
        
	}

    void Update()
    {
        if (scene.name == "AR Scene" && player.GetComponent<PlayerCombatScript>().GetTarget() != null && hasSwitched == false)
        {
            hasSwitched = true;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = music[1];
            GetComponent<AudioSource>().PlayDelayed(0);
            GetComponent<AudioSource>().loop = true;


        }
        
    }
	
}

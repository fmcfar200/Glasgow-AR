using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {


    
    public AudioClip ButtonClick;
    public AudioClip LevelUp;
    public AudioClip TargetSelect;
    public List<AudioClip> fightSounds = new List<AudioClip>();
    public List<AudioClip> music = new List<AudioClip>();

    AudioSource musicSource;

    Scene scene;

       
	// Use this for initialization
	void Start ()
    {
        musicSource = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();

        musicSource.clip = music[0];
        musicSource.Play(0);
        
	}
	
}

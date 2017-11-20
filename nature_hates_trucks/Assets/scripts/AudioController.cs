using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    // music
    public AudioClip music_menu;
    public AudioClip music_game;

    // sfx
    public AudioClip[] explosions;
    public AudioClip[] splats;
    public AudioClip[] hits;

    // options (for sound)
    public Slider slider;

    // audio source
    AudioSource sound;

	void Start () {
        sound = GetComponent<AudioSource>();
        sound.volume = 0.01f;

        musicMenu();
    }
	
	void Update () {
        sound.volume = slider.value;
	}

    // start menu music
    public void musicMenu() {
        sound.clip = music_menu;
        sound.Play();
    }

    // start game music
    public void musicGame(){
        sound.clip = music_game;
        sound.Play();
    }

    public void audioExplosion() {
        sound.PlayOneShot(explosions[Random.Range(0, explosions.Length)], 0.8f);
    }

    public void audioSplat() {
        sound.PlayOneShot(splats[Random.Range(0, splats.Length)], 1f);
    }

     public void audioHit() {
        sound.PlayOneShot(hits[Random.Range(0, hits.Length)], 1f);
    }
}



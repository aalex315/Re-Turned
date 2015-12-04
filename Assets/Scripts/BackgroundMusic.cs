using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public AudioSource audios;
	public AudioClip[] music;	

	void Awake () {
		InvokeRepeating ("PlayMusic", 1, 200);
	}
	
	void PlayMusic(){
		audios.PlayOneShot (music[Random.Range(0, music.Length)]);
	}
}

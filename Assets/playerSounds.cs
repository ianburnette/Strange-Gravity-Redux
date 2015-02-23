using UnityEngine;
using System.Collections;

public class playerSounds : MonoBehaviour {

	public AudioClip rollover, select, deselect, send, winLevel;
	public AudioSource music1, music2;
	public float maxAudio, volInc;
	bool music2on = false;

	void Start(){
		maxAudio = music1.volume;
		music2.volume = 0;
	}
	
	public void StartMusic(){
		music2on = true;
	}
	
	public void StopMusic(){
		music2on = false;
	}
	
	void Update(){
		if (music2on){
			if (music2.volume<maxAudio){
				music2.volume += volInc;
			}
		}else{
			if (music2.volume>0){
				music2.volume -= volInc/3;
			}
		}
	}
	
	public void PlayRollover(){
		audio.PlayOneShot(rollover,1);
	}
	
	public void PlaySelect(){
		audio.PlayOneShot(select,1);
	}
	
	public void PlayDeselect(){
		audio.PlayOneShot(deselect,1);
	}
	
	public void PlaySend(){
		audio.PlayOneShot(send,1);
	}
	
	public void WinLevel(){
		audio.PlayOneShot(winLevel,.1f);
	}
}

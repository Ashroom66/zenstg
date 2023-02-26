using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitle : MonoBehaviour {
	AudioSource audio;
	AudioSource bgm;
	int count = 0;
	float firstvol;

	void Start () {
		audio = GetComponent<AudioSource>();
		bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		
	}

	void Update () {
		if (count > 0) {
			count ++;
			bgm.volume = firstvol * (20f - count) * 1f / 20; 
			if (count >= 20) {
				SceneManager.LoadScene("title");
			}
		}
	}



	public void OnClick () {
		if (count == 0) {
			audio.Play();
			firstvol = bgm.volume;
			count = 1;
		}
		
		
	}
}

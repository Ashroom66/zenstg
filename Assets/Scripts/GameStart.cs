using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	Image blackoutcolor;
	RectTransform blackoutpos;
	bool clicked;
	float a = 0f;
	int count = 0;
	AudioSource audio;

	void Start () {
		GetComponent<Button>().interactable = true;
		blackoutcolor = GameObject.Find("blackout").GetComponent<Image>();
		blackoutpos = GameObject.Find("blackout").GetComponent<RectTransform>();
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		if (clicked == true) {
			if (count == 0) {
				count ++;
				blackoutpos.position = new Vector3(blackoutpos.position.x - 1500, blackoutpos.position.y, 0);
			} else if (count <= 30) {
				count ++;
				a += 1f/30f;
				blackoutcolor.color = new Color(0,0,0, a);
			} else {
				Save_Load.Resetinfo();
				// 移動
				SceneManager.LoadScene("Proto_QK");
			}
		}

	}

	public void OnClick () {
		GetComponent<Button>().interactable = false;
		clicked = true;
		audio.Play();
	}
	
}

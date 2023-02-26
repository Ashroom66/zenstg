using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_change : MonoBehaviour {
	// 要するにメニュー画面切り替え
	private bool isMain;

	public GameObject main;
	public GameObject sub;
	AudioSource se;

	void Start () {
		if(main == null) {main = GameObject.Find("Main");}
		if(sub == null)	{sub = GameObject.Find("Sub");}
		se = GetComponent<AudioSource>();
		isMain = true;
		sub.SetActive(false);
	}

	void Update() {
		if (isMain == true && (Input.GetAxisRaw("Horizontal") > 0  || Input.GetKeyDown(KeyCode.K)) ) {
			sub.SetActive(true);
			main.SetActive(false);
			isMain = false;
			se.Play();
		} else if (isMain == false && (Input.GetAxisRaw("Horizontal") < 0 || Input.GetKeyDown(KeyCode.H)) ) {
			main.SetActive(true);
			sub.SetActive(false);
			isMain = true;
			se.Play();
		}

	}
}

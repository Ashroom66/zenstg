using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextWAVE : MonoBehaviour {
	// QK時のnextwaveボタン処理
	Player player;
	int nowwave;
	int count;
	WaveEffectorQK waveeffector;
	bool clicked;
	AudioSource audio;

	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		waveeffector = GameObject.Find("waveeffector").GetComponent<WaveEffectorQK>();
		audio = GetComponent<AudioSource>();
		count = 0;
	}
	void Update () {
		if (count <= 30) {
			waveeffector.QKstart(count);
			count ++;
		} else if (count == 31) {
			count = 200;
		}

		if (clicked == true) {					// WAVE移動
			// nowwaveの値でシーン遷移先を分ける
			if (count < 230) {
				// 画面暗転処理
				waveeffector.QKfinish(count - 200);
				count ++;
			} else {
				if (nowwave == 0) {
					SceneManager.LoadScene("Proto_Area");
				} else if (nowwave == 1) {
					SceneManager.LoadScene("wave1");
				} else if (nowwave == 2) {
					SceneManager.LoadScene("wave2");
				} else if (nowwave == 3) {
					SceneManager.LoadScene("wave3");
				} else if (nowwave == 4) {
					SceneManager.LoadScene("wave4");
				} else if (nowwave == 5) {
					SceneManager.LoadScene("wave5");
				} else {
					SceneManager.LoadScene("Proto_Area");
				}
			}
		} 
	}

	public void OnClick () {
		nowwave = player.nowwave;
		Save_Load.Saveinfo();				// セーブもしとく
		GetComponent<Button>().interactable = false;
		clicked = true;
		audio.Play();
		
	}
}

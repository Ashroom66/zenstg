using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public bool active;	// アクティブ時にカウント
	private int time;	// フレーム
	public int sec;		// 秒。表示部の分単位は表示スクリプト側で処理

	void Start() {
		time = 0;
		sec = 0;
	}

	void Update() {
		if (active == true) {
			time ++;
			if (time >= 60) {
				time -= 60;
				sec ++;
			}
		}
	}

}

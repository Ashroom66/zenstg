using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveEffectorQK : MonoBehaviour {
	public Image blackout;
	public RectTransform blacktrans;
	public float a;

	public void Start () {
		blackout = GameObject.Find("blackout").GetComponent<Image>();
		blacktrans = GameObject.Find("blackout").GetComponent<RectTransform>();
	}

	public void QKstart(int time) {
		// 時間かけて透明にするだけ
		a = (30 - time) * 1f / 30f;
		blackout.color = new Color(0,0,0,a);
		if (a <= 0.01) {
			blacktrans.position = new Vector3(1300 + 640, 0 + 360, 0);
		}

	}

	public void QKfinish(int time) {
		blacktrans.position = new Vector3(640, 360, 0);
		// 時間かけて真っ黒に。
		a = time * 1f / 30f;
		blackout.color = new Color(0,0,0,a);
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_pointdisplay : MonoBehaviour {
	// 所持ポイント取得して表示するだけ。
	private Player player;
	private Text mytext;
	private string msg;
	private int pt;


	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		mytext = GetComponent<Text>();
	}

	void Update () {
		pt = player.point;
		msg = pt.ToString();
		msg += "pt";

		mytext.text = msg;
	}
}

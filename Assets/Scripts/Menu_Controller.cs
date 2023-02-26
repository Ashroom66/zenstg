using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controller : MonoBehaviour {
	private Player player;
	private GameObject menu;
	public GameObject ui;
	private int count = 0;	// 最初の30fだけはキーを受け付けない

	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		menu = GameObject.Find("Menu");
		if(ui == null) {ui = GameObject.Find("NormalUI");}
		menu.SetActive(true);
		ui.SetActive(false);
		player.canmove = false;
		Save_Load.Loadinfo();
		player.ps.hp = player.ps.maxihp;	// 体力全回復(適当)
		player.nowwave ++;					// 次の行先に指定

		
	}

	void Update() {
		if (count <= 30) {count ++;}
		// Tabキー(もっと良さげなキーあったら後々変更)でメニューON/OFF切り替え
		if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Return)) && count > 30) {
			if (player.canmove == true) {
				// メニュー展開
				menu.SetActive(true);
				ui.SetActive(false);
				player.canmove = false;
			} else {
				// メニュー非アクティブ
				menu.SetActive(false);
				ui.SetActive(true);
				player.canmove = true;
			}
		}
	}
}

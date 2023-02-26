using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_buttons : MonoBehaviour {
	public string text = "マウスクリックで選択/アンロック\nアンロック済みの副装備選択中に数字キーで装備";			// 表示テキスト
	public int unlockpt;		// アンロック必要ポイント
	public string nowselect;	// 今選択しているボタン(装備)の名前
	public bool isunlocked;		// アンロック済みか否か

	private Text textbase;
	private Text unlockbase;
	private string unlockmsg;

	private RectTransform selectframe;
	private RectTransform blueframe;	// 未選択時除去用
	private SubEquip player;			// 副装備情報
	private Player forpoint;		// プレイヤーのポイント取得用
	private Menu_subbutton sub;		// 値返したり？
	private AudioSource se;	//　装備時の音

	Button unlockbutton;	// アンロックボタンの使用可不可用

	void Start () {
		forpoint = GameObject.Find("Player").GetComponent<Player>();
		unlockbutton = GameObject.Find("unlock_button").GetComponent<Button>();
		textbase = GameObject.Find("setsumei").GetComponent<Text>();
		unlockbase = GameObject.Find("unlock_text").GetComponent<Text>();
		selectframe = GameObject.Find("yellow_frame").GetComponent<RectTransform>();
		player = GameObject.Find("Player").GetComponent<SubEquip>();
		se = GetComponent<AudioSource>();
		if (player.subname1 != "") {
			player.substate1 = 1;
			sub = GameObject.Find(player.subname1).GetComponent<Menu_subbutton>();
			sub.status = 1;
		}
		if (player.subname2!= "") {
			player.substate2 = 1;
			sub = GameObject.Find(player.subname2).GetComponent<Menu_subbutton>();
			sub.status = 2;
		}
		if (player.subname3 != "") {
			player.substate3 = 1;
			sub = GameObject.Find(player.subname3).GetComponent<Menu_subbutton>();
			sub.status = 3;
		}
		if (player.subname4 != "") {
			player.substate4 = 1;
			sub = GameObject.Find(player.subname4).GetComponent<Menu_subbutton>();
			sub.status = 4;
		}
		if (player.subname5 != "") {
			player.substate5 = 1;
			sub = GameObject.Find(player.subname5).GetComponent<Menu_subbutton>();
			sub.status = 5;
		}
		text = "マウスクリックで選択/アンロック\nアンロック済みの副装備選択中に数字キーで装備";
	}
	
	// Update is called once per frame
	void Update () {
		// 説明文更新
		textbase.text = text;
		
		// アンロックボタン更新
		if (nowselect != "") {
			if (isunlocked == true) {
				unlockmsg = "UNLOCKED";
				unlockbutton.interactable = false;
			} else {
				if (forpoint.point >= unlockpt) {
					unlockbutton.interactable = true;
				} else {
					unlockbutton.interactable = false;
				}
				unlockmsg = "UNLOCK(" + unlockpt.ToString() + "pt)";
			}
			unlockbase.text = unlockmsg;
		}

		// 黄色い枠君移動
		if (nowselect != "") {
			selectframe.position = GameObject.Find(nowselect).GetComponent<RectTransform>().position;
		}

		// 不要な青枠君除去
		if (player.subname1 == "") {
			blueframe = GameObject.Find("lblue_frame1").GetComponent<RectTransform>();
			blueframe.position = new Vector3(-100, 0, 0);
		}
		if (player.subname2 == "") {
			blueframe = GameObject.Find("lblue_frame2").GetComponent<RectTransform>();
			blueframe.position = new Vector3(-100, 0, 0);
		}
		if (player.subname3 == "") {
			blueframe = GameObject.Find("lblue_frame3").GetComponent<RectTransform>();
			blueframe.position = new Vector3(-100, 0, 0);
		}
		if (player.subname4 == "") {
			blueframe = GameObject.Find("lblue_frame4").GetComponent<RectTransform>();
			blueframe.position = new Vector3(-100, 0, 0);
		}
		if (player.subname5 == "") {
			blueframe = GameObject.Find("lblue_frame5").GetComponent<RectTransform>();
			blueframe.position = new Vector3(-100, 0, 0);
		} 

		// 装備
		// 選択中に1～5のどれかのキーが表示されたから

		// 装備後ダブりは排除する
		// 元あった装備の装備状況を戻す作業もここで。
		if (isunlocked == true) {
			if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) {
				if (player.subname1 != "" && player.subname1 != null) {
					sub = GameObject.Find(player.subname1).GetComponent<Menu_subbutton>();
					sub.status = 0;
				}
				player.subname1 = nowselect;
				player.substate1 = 1;
				if (player.subname2 == nowselect) {player.subname2 = ""; player.substate2 = 0;}
				if (player.subname3 == nowselect) {player.subname3 = ""; player.substate3 = 0;}
				if (player.subname4 == nowselect) {player.subname4 = ""; player.substate4 = 0;}
				if (player.subname5 == nowselect) {player.subname5 = ""; player.substate5 = 0;}
				if (nowselect != "") {
					sub = GameObject.Find(nowselect).GetComponent<Menu_subbutton>();
					sub.status = 1;
					se.Play();
				}
				
				player.nowequip = 1;
				player.callweapon = player.subname1;
				
			}
			if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) {
				if (player.subname2 != "" && player.subname2 != null) {
					sub = GameObject.Find(player.subname2).GetComponent<Menu_subbutton>();
					sub.status = 0;
				}
				player.subname2 = nowselect;
				player.substate2 = 1;
				if (player.subname1 == nowselect) {player.subname1 = ""; player.substate1 = 0;}
				if (player.subname3 == nowselect) {player.subname3 = ""; player.substate3 = 0;}
				if (player.subname4 == nowselect) {player.subname4 = ""; player.substate4 = 0;}
				if (player.subname5 == nowselect) {player.subname5 = ""; player.substate5 = 0;}
				if (nowselect != "") {
					sub = GameObject.Find(nowselect).GetComponent<Menu_subbutton>();
					sub.status = 2;
					se.Play();
				}
				player.nowequip = 2;
				player.callweapon = player.subname2;
			}
			if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) {
				if (player.subname3 != "" && player.subname3 != null) {
					sub = GameObject.Find(player.subname3).GetComponent<Menu_subbutton>();
					sub.status = 0;
				}
				player.subname3 = nowselect;
				player.substate3 = 1;
				if (player.subname1 == nowselect) {player.subname1 = ""; player.substate1 = 0;}
				if (player.subname2 == nowselect) {player.subname2 = ""; player.substate2 = 0;}
				if (player.subname4 == nowselect) {player.subname4 = ""; player.substate4 = 0;}
				if (player.subname5 == nowselect) {player.subname5 = ""; player.substate5 = 0;}
				if (nowselect != "") {
					sub = GameObject.Find(nowselect).GetComponent<Menu_subbutton>();
					sub.status = 3;
					se.Play();
				}
				player.nowequip = 3;
				player.callweapon = player.subname3;
			}
			if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) {
				if (player.subname4 != "" && player.subname4 != null) {
					sub = GameObject.Find(player.subname4).GetComponent<Menu_subbutton>();
					sub.status = 0;
				}
				player.subname4 = nowselect;
				player.substate4 = 1;
				if (player.subname1 == nowselect) {player.subname1 = ""; player.substate1 = 0;}
				if (player.subname2 == nowselect) {player.subname2 = ""; player.substate2 = 0;}
				if (player.subname3 == nowselect) {player.subname3 = ""; player.substate3 = 0;}
				if (player.subname5 == nowselect) {player.subname5 = ""; player.substate5 = 0;}
				if (nowselect != "") {
					sub = GameObject.Find(nowselect).GetComponent<Menu_subbutton>();
					sub.status = 4;
					se.Play();
				}
				player.nowequip = 4;
				player.callweapon = player.subname4;
			}
			if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) {
				if (player.subname5 != "" && player.subname5 != null) {
					sub = GameObject.Find(player.subname5).GetComponent<Menu_subbutton>();
					sub.status = 0;
				}
				player.subname5 = nowselect;
				player.substate5 = 1;
				if (player.subname1 == nowselect) {player.subname1 = ""; player.substate1 = 0;}
				if (player.subname2 == nowselect) {player.subname2 = ""; player.substate2 = 0;}
				if (player.subname3 == nowselect) {player.subname3 = ""; player.substate3 = 0;}
				if (player.subname4 == nowselect) {player.subname4 = ""; player.substate4 = 0;}
				if (nowselect != "") {
					sub = GameObject.Find(nowselect).GetComponent<Menu_subbutton>();
					sub.status = 5;
					se.Play();
				}
				player.nowequip = 5;
				player.callweapon = player.subname5;
			}

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEquip : MonoBehaviour {

	private Player player;

	// 主にStart内で使用
	public string subname1;	// 副装名
	public string subname2;
	public string subname3;
	public string subname4;
	public string subname5;
	public int substate1;	// 副装装備状態+レベル
	public int substate2;	// 装備可能か否か(装備不可=0)
	public int substate3;
	public int substate4;
	public int substate5;
	public int lvl;			// 呼び出し時の変数参照用
	public int nowequip;	// 現在の装備(0は未装備		多分表示用
	AudioSource[] se;
	private int oldlvl;
	private int oldnowequip;

	// Update内で使う
	int key;				// 数字キー判定用
	public string callweapon;		// 呼び出す装備name(メニューボタンによる装備更新の為public化)

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		se = GetComponents<AudioSource>();
		nowequip = 0;
		callweapon = "";

		// 自動装備
		if(substate1 != 0) {
			callweapon = subname1;
			nowequip = 1;
			lvl = substate1;
		} else if(substate2 != 0) {
			callweapon = subname2;
			nowequip = 2;
			lvl = substate2;
		} else if(substate3 != 0) {
			callweapon = subname3;
			nowequip = 3;
			lvl = substate3;
		} else if(substate4 != 0) {
			callweapon = subname4;
			nowequip = 4;
			lvl = substate4;
		} else if(substate5 != 0) {
			callweapon = subname5;
			nowequip = 5;
			lvl = substate5;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		// キー入力受付:ガトリングバグ対策にcanmoveがfalseの時は受け付けない

		if (substate1 != 0 && player.canmove == true && (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6))) {
			if (nowequip != 1) {se[0].Play();}
			callweapon = subname1;
			nowequip = 1;
			lvl = substate1;
		} else if (substate2 != 0 && player.canmove == true && (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha7) || Input.GetKey(KeyCode.Keypad7))) {
			if (nowequip != 2) {se[0].Play();}
			callweapon = subname2;
			nowequip = 2;
			lvl = substate2;
		} else if (substate3 != 0 && player.canmove == true && (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8))) {
			if (nowequip != 3) {se[0].Play();}
			callweapon = subname3;
			nowequip = 3;
			lvl = substate3;
		} else if (substate4 != 0 && player.canmove == true && (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Alpha9) || Input.GetKey(KeyCode.Keypad9))) {
			if (nowequip != 4) {se[0].Play();}
			callweapon = subname4;
			nowequip = 4;
			lvl = substate4;
		} else if (substate5 != 0 && player.canmove == true && (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Alpha0) || Input.GetKey(KeyCode.Keypad0))) {
			if (nowequip != 5) {se[0].Play();}
			callweapon = subname5;
			nowequip = 5;
			lvl = substate5;
		}

		

		// レベルチェック
		if (nowequip == 1) {
			lvl = substate1;
		} else if (nowequip == 2) {
			lvl = substate2;
		} else if (nowequip == 3) {
			lvl = substate3;
		} else if (nowequip == 4) {
			lvl = substate4;
		} else if (nowequip == 5) {
			lvl = substate5;
		}

		// callweaponとか使う 
//		Subcon_speed1 sub = GetComponent(callweapon) as Subcon_speed1;
		Subcon sub = GetComponent<Subcon>();
		sub.Shot(callweapon, lvl);

		if (oldnowequip == nowequip && oldlvl < lvl) {
			se[7].Play();
		}

		oldnowequip = nowequip;
		oldlvl = lvl;
		

	}
}

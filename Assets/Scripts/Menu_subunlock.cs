using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_subunlock : MonoBehaviour {

	private Menu_buttons info;
	private Menu_subbutton sub;
	private Player player;
	private string nowselect;	// 現在選択中の副装備名
	private int cost;			// アンロック必要pt
	private int own;			// プレイヤー所持pt
	private bool isunlocked;	
	private AudioSource se;
	private SubEquip subequip;	// 自動装備用

	void Start () {
		info = GameObject.Find("Buttons").GetComponent<Menu_buttons>();
		player = GameObject.Find("Player").GetComponent<Player>();
		se = GetComponent<AudioSource>();
		subequip = GameObject.Find("Player").GetComponent<SubEquip>();
	}

	public void OnClick () {
		// 一応buttons君で判定はしてあるけどここでも再チェック
		nowselect = info.nowselect;
		sub = GameObject.Find(nowselect).GetComponent<Menu_subbutton>();
		if (sub.status == -1) {
			isunlocked = false;
		} else {
			isunlocked = true;	// アンロック済み
		}
		cost = info.unlockpt;
		own = player.point;
		if (own >= cost && isunlocked == false) {
			// アンロック
			player.point -= cost;
			sub.status = 0;
			se.Play();
			
			// 自動装備
			if(subequip.subname1 == "") {
				subequip.subname1 = nowselect;
				subequip.substate1 = 1;
			} else if(subequip.subname2 == "") {
				subequip.subname2 = nowselect;
				subequip.substate2 = 1;
			} else if(subequip.subname3 == "") {
				subequip.subname3 = nowselect;
				subequip.substate3 = 1;
			} else if(subequip.subname4 == "") {
				subequip.subname4 = nowselect;
				subequip.substate4 = 1;
			} else if(subequip.subname5 == "") {
				subequip.subname5 = nowselect;
				subequip.substate5 = 1;
			}
		}
	}
	


}

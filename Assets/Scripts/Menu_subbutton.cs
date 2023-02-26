using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_subbutton : MonoBehaviour {
	public int status;	// -1:未アンロック　0:未装備　1-5:装備no
	public int needpt;	// アンロック必要ポイント
	public string name;	// 装備名
	[TextArea]public string message; // 選択時表示テキスト
	private bool isunlocked = false; // アンロック時に鍵画像取っ払うときに使用
	AudioSource se;

	private RectTransform rec;
	Menu_buttons buttons;

	public void Start(){
		se = GetComponent<AudioSource>();
		buttons = GameObject.Find("Buttons").GetComponent<Menu_buttons>();
		Save_Load.Subload(gameObject.name);
		
	}

	public void Update() {
		if (status > 0) {
			string s = "lblue_frame" + status.ToString();
			rec = GameObject.Find(s).GetComponent<RectTransform>();
			rec.position = this.GetComponent<RectTransform>().position;
		}

		if (isunlocked == false && status != -1) {
			// 鍵君削除
			Destroy(transform.Find("lock").gameObject);
			isunlocked = true;
			buttons.isunlocked = true;
		}
		Save_Load.Subsave(gameObject.name);

	}

	public void OnClick () {
		if (status >= 0) {
			buttons.isunlocked = true;
		} else {
			buttons.isunlocked = false;
		}
		se.Play();
		buttons.text = message;
		buttons.unlockpt = needpt;
		buttons.nowselect = name;



	}
	
}

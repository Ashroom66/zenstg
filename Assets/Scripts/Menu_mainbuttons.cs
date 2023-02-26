using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_mainbuttons : MonoBehaviour {
	public bool isup;	// アップグレードか否か

	private bool reverse;	// 強化方向が下ならtrue(親から取得)
	private int cost;	// コスト値
	private Menu_mains oya;	// 我が親オブジェ(コスト目当て)	
	private Player player;	// プレイヤー情報(コスト目当て)
	private string oyaname;	// 一部の項目はn倍している為
	private int bairitsu = 1;
	private int pushcount;	// 長押し30f以上で連続処理
	private bool push = false;		// 押し状況
	private Button button;
	private bool active;			// そもそもボタンがアクティブかどうか
	
	void Start () {
		oya = transform.parent.gameObject.GetComponent<Menu_mains>();
		button = GetComponent<Button>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reverse = oya.lowmax;

		oyaname = transform.parent.gameObject.transform.name;
		if (oyaname == "maxiV") {bairitsu = 5;}
		if (oyaname == "bulletspeed") {bairitsu = 2;}
		if (oyaname == "recoil") {bairitsu = 10;}
		if (transform.name == "upgrade") {isup = true;}
	}

	void Update() {
		active = button.interactable;
		if (push == true && active == true) {
			if (pushcount == 0 || (pushcount >= 30 && pushcount % 2 == 0)) {
				cost = oya.costvalue;	// ここで更新するのでmain途中からコスト変えても大丈夫 :p
				if (isup == true) {
					// アップグレード
					if (reverse == false) {
						// 通常アップグレード
						oya.ButtonPushed (1f/bairitsu);
						
					} else {
						// 反転アップグレード(値を小さく)
						oya.ButtonPushed ((1f/bairitsu) * -1);
					}
					player.point -= cost;
				} else {
					// ダウングレード
					if (reverse == false) {
						// 通常ダウングレード
						oya.ButtonPushed ((1f/bairitsu) * -1);
					} else {
						// 反転ダウングレード(値を大きく)
						oya.ButtonPushed (1f/bairitsu);
					}
					player.point += cost;
				}

			}
			pushcount ++;
		}
	}

	public void PointerDown() {
		// ボタン押してる
		push = true;
	}
	public void PointerUp() {
		// ボタン押してないときの処理：要するにpushcount初期化
		pushcount = 0;
		push = false;
	}

}

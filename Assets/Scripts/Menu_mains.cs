using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_mains : MonoBehaviour {
	// 基本的に表示関連
	public string msgname;	// 項目名
	public int min_limit;	// 強化下限
	public int now;
	public int max_limit;	// 強化上限
	public int costvalue;	// 強化/還元時変動pt
	public bool lowmax;		// 下限の方が強い場合にコレ(upgradeの役割とか切り替える)

	private string valuemsg;	// 〇〇/〇〇作成
	private string myname;		// 僕の名は。
	private GameObject player;
	AudioSource audio;
	
	

	// 子オブジェ共
	Text msg;
	Text valuetext;
	Text cost;
	Button down;
	Button up;
	void Start () {
		audio = GetComponent<AudioSource>();
		msg =  transform.Find("msg").gameObject.GetComponent<Text>();
		valuetext = transform.Find("valuetext").gameObject.GetComponent<Text>();
		cost = transform.Find("cost").gameObject.GetComponent<Text>();
		down = transform.Find("downgrade").gameObject.GetComponent<Button>();
		up = transform.Find("upgrade").gameObject.GetComponent<Button>();
		player = GameObject.Find("Player");
	}


	void Update () {
		// 主人公の各要素取得
		myname = transform.name;
		// nowの値決める
		// どうせ10個止まりなので一つ一つやる(脳筋)
		if (myname == "hp") {
			now = player.GetComponent<Player>().ps.maxihp;
		} else if (myname == "maxiV") {
			now = (int)(player.GetComponent<Player>().ps.maxiV * 5);
		} else if (myname == "atk_boost") {
			now = player.GetComponent<Player>().ps.atk_boost;
		} else if (myname == "avoidance") {
			now = (int)player.GetComponent<Player>().avoidance;
		} else if (myname == "avo_int") {
			now = player.GetComponent<Player>().avo_interval;
		} else if (myname == "atk") {
			now = player.GetComponent<Player>().ps.atk;
		} else if (myname == "ct") {
			now = player.GetComponent<BulletBase>().ct;
		} else if (myname == "deletetime") {
			now = (int)player.GetComponent<BulletBase>().delValue;
		} else if (myname == "bulletspeed") {
			now = (int)(player.GetComponent<BulletBase>().speed * 2);
		} else if (myname == "recoil") {
			now = (int)(player.GetComponent<BulletBase>().recoil * 10);
		}

		// 表示系
		msg.text = msgname;
		valuemsg = now.ToString() + "/" + max_limit.ToString();

		cost.text = "Cost:" + costvalue.ToString() + "pt";
		if (lowmax == true) {
			valuemsg = now.ToString() + "/" + min_limit.ToString();
		} else {
			valuemsg = now.ToString() + "/" + max_limit.ToString();
		}
		valuetext.text = valuemsg;
		
		if (lowmax == true) {
			// リバースの処理
			if ((down.interactable == true && max_limit <= now)) {
				// 強化下限
				down.interactable = false;
			} else if (down.interactable == false && now < max_limit) {
				down.interactable = true;
			}
			if (up.interactable == true && now <= min_limit || player.GetComponent<Player>().point < costvalue) {
				up.interactable = false;
			} else if (up.interactable == false && min_limit < now && player.GetComponent<Player>().point >= costvalue) {
				up.interactable = true;
			}

		} else {
			// 普通の処理
			if (down.interactable == true && now <= min_limit) {
				// 強化下限：down君を操作不可能にする
				down.interactable = false;
			} else if (down.interactable == false && min_limit < now) {
				// 強化下限解除
				down.interactable = true;
			}
			if ((up.interactable == true && max_limit <= now) || player.GetComponent<Player>().point < costvalue) {
				// 強化上限：up君操作不可能に
				up.interactable = false;
			} else if (up.interactable == false && now < max_limit && player.GetComponent<Player>().point >= costvalue) {
				// 強化可能
				up.interactable = true;
			}
		}
		
	}

	public void ButtonPushed(float a) {
		int b = (int)a;	// モノによってはintonlyなやつもあるので個別に対応
		audio.Play();
		if (myname == "hp") {
			player.GetComponent<Player>().ps.maxihp += b;
			player.GetComponent<Player>().ps.hp = player.GetComponent<Player>().ps.maxihp;
		} else if (myname == "maxiV") {
			player.GetComponent<Player>().ps.maxiV += a;
		} else if (myname == "atk_boost") {
			player.GetComponent<Player>().ps.atk_boost += b;
		} else if (myname == "avoidance") {
			player.GetComponent<Player>().avoidance += a;
		} else if (myname == "avo_int") {
			player.GetComponent<Player>().avo_interval += b;
		} else if (myname == "atk") {
			player.GetComponent<Player>().ps.atk += b;
		} else if (myname == "ct") {
			player.GetComponent<BulletBase>().ct += b;
		} else if (myname == "deletetime") {
			player.GetComponent<BulletBase>().delValue += a;
		} else if (myname == "bulletspeed") {
			player.GetComponent<BulletBase>().speed += a;
		} else if (myname == "recoil") {
			player.GetComponent<BulletBase>().recoil += a;
		}

	} 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	// テンプレ要素の組み合わせ：BulletBaseと同じような使い道
	// いくつかの基礎動作も用意？？

	// 基礎ステータス 攻撃力と体力
	public int hp;
	public int atk;			// 接触時の与ダメージ
	public int atkb;		// 弾系攻撃力(予備)

	// 移動・索敵
	public float speed;
	public float subspeed;	// 予備
	public float search;	// 索敵範囲(範囲内にプレイヤーinでパターン切り替え)
	public bool active;		// 索敵範囲内にplayerいる=true

	// 撃破時のスコアと強化ポイント
	public int score;
	public int pt;

	// 被弾時クールタイム
	private int hitct;

	// プレイヤーとの距離取得用
	private Vector3 playerpos;

	// ｽｺｱ加算とか
	DamageUI damageui;
	AddScores adder;
	private bool dying;

	AudioSource[] se;

	// 生成されたか否か(スコア非干渉、弾レベルだけ反映)
	public bool isgenerated;

	void Start () {
		damageui = GameObject.Find("DamageUI").GetComponent<DamageUI>();
		adder = GameObject.Find("PS_adder").GetComponent<AddScores>();
		se = GetComponents<AudioSource>();
		hitct = 2;
	}
	void Update() {
		if (hitct >= 0) { hitct --; }	// 被ダメ時CTカウント


		// 索敵→アクティブ/非アクティブ判断
		playerpos = GameObject.Find("Player").transform.position;
		if ((playerpos - this.transform.position).magnitude <= search) {
			active = true;
		} else {
			active = false;
		}
	}

	void OnTriggerStay2D (Collider2D c) {
		if (hitct <= 0 && (c.gameObject.name.Contains("pierce2")) || c.gameObject.name.Contains("charge")) {
			// メガフレア・突撃野郎他処理
			hitct = 3;
			int hpdelta = c.GetComponent<BulletBase>().dmg;

			hp -= hpdelta;
			GameObject.Find("DamageUI").GetComponent<DamageUI>().DmgPopUp(transform.position, hpdelta);
			se[0].Play();
			if (hp <= 0 && dying == false) {
				dying = true;			// ｽｺｱ二重取得防止
				if (c.gameObject.tag == "PlayerBulletSub") {
					adder.Add_score(score, true, true, isgenerated, c.gameObject.name);
				} else {
					adder.Add_score(score ,true, false, isgenerated);
				}
				
				adder.Add_point(pt);
				GameObject.Find("ndk").transform.position = transform.position;
				GameObject.Find("ndk").GetComponent<AudioSource>().Play();
				Destroy(this.gameObject);
			}
		} 
		
	}

	void OnTriggerEnter2D (Collider2D c) {
		if (hitct <= 0 && (c.gameObject.tag == "PlayerBullet" || c.gameObject.tag == "PlayerBulletSub") && c.gameObject.name.Contains("pierce2") == false && c.gameObject.name.Contains("charge") == false ) {
			// ダメージ考慮はこっちだが、弾消す処理は弾の方
			// (貫通/非貫通弾の存在)
			hitct = 0;

			int hpdelta = c.GetComponent<BulletBase>().dmg;

			hp -= hpdelta;
			damageui.DmgPopUp(c.transform.position, hpdelta);
			se[0].Play();
			if (hp <= 0 && dying == false) {
				dying = true;			// ｽｺｱ二重取得防止
				if (c.gameObject.tag == "PlayerBulletSub") {
					adder.Add_score(score, true, true, isgenerated, c.gameObject.name);
				} else {
					adder.Add_score(score ,true, false, isgenerated);
				}
				
				adder.Add_point(pt);
				GameObject.Find("ndk").transform.position = transform.position;
				GameObject.Find("ndk").GetComponent<AudioSource>().Play();
				Destroy(this.gameObject);
			}

		}
	}

	/*
	void OnTriggerStay2D (Collider2D c) {
		if (hitct <= 0 && (c.gameObject.name.Contains("pierce2")) || c.gameObject.name.Contains("charge")) {
			// メガフレア・突撃野郎他処理
			hitct = 3;
			int hpdelta = c.GetComponent<BulletBase>().dmg;

			hp -= hpdelta;
			GameObject.Find("DamageUI").GetComponent<DamageUI>().DmgPopUp(transform.position, hpdelta);
			se[0].Play();
			if (hp <= 0 && dying == false) {
				dying = true;			// ｽｺｱ二重取得防止
				if (c.gameObject.tag == "PlayerBulletSub") {
					adder.Add_score(score, true, true, isgenerated, c.gameObject.name);
				} else {
					adder.Add_score(score ,true, false, isgenerated);
				}
				
				adder.Add_point(pt);
				GameObject.Find("ndk").transform.position = transform.position;
				GameObject.Find("ndk").GetComponent<AudioSource>().Play();
				Destroy(this.gameObject);
			}
		} else if (hitct <= 0 && (c.gameObject.tag == "PlayerBullet" || c.gameObject.tag == "PlayerBulletSub") ) {
			// ダメージ考慮はこっちだが、弾消す処理は弾の方
			// (貫通/非貫通弾の存在)
			hitct = 0;

			int hpdelta = c.GetComponent<BulletBase>().dmg;

			hp -= hpdelta;
			damageui.DmgPopUp(c.transform.position, hpdelta);
			se[0].Play();
			if (hp <= 0 && dying == false) {
				dying = true;			// ｽｺｱ二重取得防止
				if (c.gameObject.tag == "PlayerBulletSub") {
					adder.Add_score(score, true, true, isgenerated, c.gameObject.name);
				} else {
					adder.Add_score(score ,true, false, isgenerated);
				}
				
				adder.Add_point(pt);
				GameObject.Find("ndk").transform.position = transform.position;
				GameObject.Find("ndk").GetComponent<AudioSource>().Play();
				Destroy(this.gameObject);
			}

		}
		
	}
	 */

	 
}

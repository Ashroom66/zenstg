using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBasic : MonoBehaviour {
	// ベース呼び出し準備
	BulletBase bulletbase;

	// 弾情報はプレイヤー側から取得。
	// プレイヤーの弾情報を探す変数
	BulletBase player;


	void Start () {
		// ベース呼び出し
		bulletbase = GetComponent<BulletBase>();

		// プレイヤー発見
		player = GameObject.Find("Player").GetComponent<BulletBase>();

		// 弾設定はプレイヤーのデータを引き継ぐ
		bulletbase.speed = player.speed;
		bulletbase.delMode = player.delMode;
		bulletbase.delValue = player.delValue;
		bulletbase.dmg = (int)(GameObject.Find("Player").GetComponent<Player>().ps.atk * ((100f +  GameObject.Find("Player").GetComponent<Player>().ps.atk_boost)/100));

		// 弾動かす
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * bulletbase.speed;
	}

	void Update () {
		// Delete
		bulletbase.Delete(bulletbase.delMode, bulletbase.delValue);
	}

	void OnTriggerStay2D (Collider2D c) {
		if (c.gameObject.tag == "Enemybody") {
			Destroy(this.gameObject);
		}
	}
}

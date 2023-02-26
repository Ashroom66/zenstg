using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submove_rectilinear : MonoBehaviour {
	// 直進する基本系

	// ベース呼び出し準備
	BulletBase bulletbase;

	// 弾情報：弾ごとに設定
	// 変数は火力、delmode、delvalue、貫通性有無、ばらつきを予定。
	// 後々追加するかも。
	public int atk;			// 攻撃力	bulletbaseのダメージを計算に入れるが、player側の攻撃ブーストを考慮する為に敢えてここに別枠として基礎攻撃力を置いておく
	public float speed;		// 弾速
	public int delMode;		
	public float delValue;
	public bool pierce;
	public bool isEnemyBullet;	// 敵の弾か否か(衝突判定時に使用)
	public bool isShotGun;		// ショットガンの時は拡大率とか色々いじる為
	public bool isP2;			// 貫通の2：メガフレア
	public float birth;			// メガフレア他：生成(極大までの)時間
	public float size;
	private float count;
	private float sizenow;


	// 火力ブースト考慮用
	private Player player;

	void Start () {
		// ベース君呼び出し
		bulletbase = GetComponent<BulletBase>();
		// バレットベースへ火力適用
		if (gameObject.tag != "EnemyBullet") {
			player = GameObject.Find("Player").GetComponent<Player>();
			bulletbase.dmg = (int)(atk * ((player.ps.atk_boost + 100f)/100));
		} else {
			bulletbase.dmg = atk;
		}

		// ショットガン処理
		if (isShotGun == true) {
			float bairitu = Random.Range(0.3f, 1f);
			transform.localScale = new Vector3(bairitu, bairitu, 1f);
			speed *= Random.Range(0.2f, 1f);
			delValue *= Random.Range(0.2f, 1f);
			bulletbase.dmg = (int)(bulletbase.dmg * Random.Range(0.1f, 1f));
		}

		// メガフレア処理
		if (isP2 == true) {
			count = birth * -1;
		}
		// 弾動かす
		// 直進 :D
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;

	}

	void Update () {

		// Delete herself
		bulletbase.Delete(delMode, delValue + birth);
		// メガフレア処理
		if (isP2 == true) {
			count ++;
			if (count < 0) {
				// 生成
				sizenow = size * ((birth + count)/ birth) * ((birth + count)/ birth);
			} else {
				sizenow = size * ( 1 -  (count / delValue) * (count / delValue));
			}
			transform.localScale = new Vector3(sizenow, sizenow, 1);
		}
	}

	
	void OnTriggerStay2D (Collider2D c) {
		if ((isEnemyBullet == true && c.gameObject.tag == "Playerbody") || (isEnemyBullet == false && c.gameObject.tag == "Enemybody"　&& pierce == false)) {
			Destroy(this.gameObject);
		}
		
	}

}


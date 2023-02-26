using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submove_orbit : MonoBehaviour {
	// プレイヤーの周囲を回転する。体力を自身の回転速度として、敵の弾を撃ち消す。
	// 不幸にも敵に追突してしまったら消滅

	// ベース呼び出し準備
	BulletBase bulletbase;

	// 弾情報
	public int atk;			// 攻撃力	bulletbaseのダメージを計算に入れるが、player側の攻撃ブーストを考慮する為に敢えてここに別枠として基礎攻撃力を置いておく
	public int delMode;		
	public float delValue;
	public int hp;			// 体力：弾打消し回数兼自転速度
	public float period;	// マイナス値入れると公転逆回転
	public float rx;
	public float ry;
	public bool riv;		// 自転角度反転させるか否か
	private float starttheta;	// start時の回転角度から決める
	public float rotate;
	private float theta;
	private float x;
	private float y;
	public bool isperiphery;
	private GameObject player;
	private Vector3 originpos;
	private Vector3 mypos;
	private AddScores adder;
	private int ct;
	private AudioSource[] se;


	// 火力ブースト考慮用
	private Player pl;

	void Start () {
		// ベース君他呼び出し
		bulletbase = GetComponent<BulletBase>();
		player = GameObject.Find("Player");
		// バレットベースへ火力適用
		pl = player.GetComponent<Player>();
		bulletbase.dmg = (int)(atk * ((pl.ps.atk_boost + 100f)/100));

		starttheta = transform.localEulerAngles.z;
		if (isperiphery == true) {
			starttheta += 30;	// ちょっと角度足す
		}
		theta = starttheta;
		adder = GameObject.Find("PS_adder").GetComponent<AddScores>();
		se = GetComponents<AudioSource>();
	}

	void Update () {
		// Delete herself
		bulletbase.Delete(delMode, delValue);
		ct ++;
		// 消滅間近になると点滅
		if ( delValue - ct < 90) {
			float a = Mathf.Abs(Mathf.Sin(Time.time * 1000));
			this.GetComponent<SpriteRenderer> ().color =  new Color(1f,1f,1f,a);
		}
		
		// 動作は基本的に ど、どうしたんだい唐澤貴洋君　そんなに大声出して
		// 自転
		if (riv == true) {
			transform.Rotate(0,0,hp * -1);
		} else {
			transform.Rotate(0,0,hp);
		}
		

		// 公転
		originpos = player.transform.position;
		x = originpos.x +  (rx * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Cos(rotate * Mathf.PI / 180f) - ry * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Sin(rotate * Mathf.PI / 180f));
		y = originpos.y + (rx * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Sin(rotate * Mathf.PI / 180f) + ry * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Cos(rotate * Mathf.PI / 180f));
		transform.position = new Vector3(x, y, 0);
		theta += 360f / (period * 60f);

	}

	
	void OnTriggerEnter2D (Collider2D c) {
		if (c.gameObject.tag == "Enemybody") {
			Destroy(this.gameObject);
		} else if (c.gameObject.tag == "EnemyBullet") {
			Destroy(c.gameObject);
			hp --;
			se[1].Play();
			adder.Add_score(6, true, true, true, "hexashield");	// 弾弾くとボーナス :)
			if (hp <= 0) {
				Destroy(this.gameObject);
			}
		}
	}

}


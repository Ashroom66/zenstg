using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// 壁。性格には斥力場。弾だけ通る
	// 侵入時に角度を取得、stayで算出した角度からベクトル計算しAddForceやる

	// 弾も通らない普通の壁実装しても良いかもね

	/*
	とぅーどぅー
	・円形・矩形disable
	*/

	public float maxidist;		// 最大出力到達点までの距離
	public float maxipow;		// 最大出力
	public bool deletepbullet;	// プレイヤー弾消すかどうか
	public bool deleteebullet;	// 敵の弾消すかどうか
	public bool affectbullet;	// 弾に影響与えるか否か
	public bool affectbody;		// ぼでーに影響与えるか否か
	public bool islect;	// 四角形か否か。チェック外せばただの丸扱い
	public bool daUp;	// 矩形専用disable、上判定を無にする
	public bool daRight;// →
	public bool daLeft;	// ←	チェック時はその方向の判定を"円"とする
	public bool daDown;	// ↓

	public float rotspd;

	private Vector2 mypos;
	private Vector2 tgtpos;
	private float dist;
	private float maxid;

	private Vector3 force;

	// 矩形時使用
	public int direction;	// テンキーをイメージ
	private float rotz;		// 自分の角度
	private SpriteRenderer mine;
	private BoxCollider2D minesquare;
	private float raitox;	// 自分のサイズ→"離れ具合"
	private float raitoy;
	private bool da = false;

	void Start () {
		mine = gameObject.GetComponent<SpriteRenderer>();
		if(islect == true) { minesquare = gameObject.GetComponent<BoxCollider2D>(); }
	}

	void Update () {
		transform.Rotate(new Vector3(0,0,rotspd / 10));
		if(islect == true) {
			mine.size = new Vector2(minesquare.size.x + 0.33f, minesquare.size.y + 0.33f);
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		// 弾消去関連
		if(deletepbullet == true && c.gameObject.tag.Contains("PlayerBullet")) { Destroy(c.gameObject); }
		if(deleteebullet == true && c.gameObject.tag.Contains("EnemyBullet")) { Destroy(c.gameObject); }
		if (islect == true) {
			// 方向計算
			rotz = transform.localEulerAngles.z * -1;
			raitox = mine.size.x;
			raitoy = mine.size.y;
			mypos = transform.position;
			tgtpos = c.transform.position;	// tgtposはこの後rotzで座標補正を行う
			tgtpos = new Vector2(tgtpos.x - mypos.x, tgtpos.y - mypos.y);	// 相対座標化
			tgtpos = new Vector2(tgtpos.x * Mathf.Cos(rotz * Mathf.PI / 180) + tgtpos.y * Mathf.Cos((rotz + 90) * Mathf.PI / 180), tgtpos.x * Mathf.Sin(rotz * Mathf.PI / 180) + tgtpos.y * Mathf.Sin((rotz + 90) * Mathf.PI / 180));	// 角度補正
			raitox = Mathf.Abs(tgtpos.x) / raitox;
			raitoy = Mathf.Abs(tgtpos.y) / raitoy;
			if(raitox > raitoy) {
				// 横
				if(tgtpos.x > 0) {
					direction = 6;
				} else {
					direction = 4;
				}
			} else {
				// 縦
				if(tgtpos.y > 0) {
					direction = 8;
				} else {
					direction = 2;
				}
			}

			// da判定
			da = false;
			if(direction == 2 && daDown == true) {da = true;}
			if(direction == 4 && daLeft == true) {da = true;}
			if(direction == 6 && daRight == true) {da = true;}
			if(direction == 8 && daUp == true) {da = true;}
		}
	}

	void OnTriggerStay2D(Collider2D c) {
		// affect外し関係
		if((affectbullet == false && c.gameObject.tag.Contains("Bullet")) || (affectbody == false && c.gameObject.tag.Contains("body"))) { 

		} else {
			// ほんへ
			if (islect == true && da == false) {
				// 矩形able
				rotz = transform.localEulerAngles.z * -1;
				if(direction == 2) {rotz += 180;}
				if(direction == 4) {rotz -= 90;}
				if(direction == 6) {rotz += 90;}
				if(direction == 8) {rotz += 0;}
				raitox = mine.size.x / 100;
				raitoy = mine.size.y / 100;
				mypos = transform.position;
				tgtpos = c.transform.position;	// tgtposはこの後rotzで座標補正を行う
				tgtpos = new Vector2(tgtpos.x - mypos.x, tgtpos.y - mypos.y);	// 相対座標化
				tgtpos = new Vector2(tgtpos.x * Mathf.Cos(rotz * Mathf.PI / 180) + tgtpos.y * Mathf.Cos((rotz + 90) * Mathf.PI / 180), tgtpos.x * Mathf.Sin(rotz * Mathf.PI / 180) + tgtpos.y * Mathf.Sin((rotz + 90) * Mathf.PI / 180));	// 角度補正
				if(direction == 2 || direction == 8) {
					// 縦
					dist = Mathf.Abs(tgtpos.y);
					maxid = Mathf.Abs((float)(raitoy / 2.0 - maxidist));
					dist = Mathf.Abs(raitoy / 2 - dist) / Mathf.Abs(raitoy/2 - maxid);
					if(dist > 1f) {dist = 1f;}
				} else {
					// 横

					dist = Mathf.Abs(tgtpos.x);
					maxid = Mathf.Abs((float)(raitox / 2.0 - maxidist));
					dist = Mathf.Abs(raitox / 2 - dist) / Mathf.Abs(raitox/2 - maxid);
					if(dist > 1f) {dist = 1f;}
				}
				rotz *= -1;
				force = new Vector3(Mathf.Cos((90 + rotz) * Mathf.PI / 180),Mathf.Sin((90 + rotz) * Mathf.PI / 180),0);
				c.GetComponent<Rigidbody2D>().AddForce(force * dist * maxipow / 10, ForceMode2D.Impulse);

			} else {
				// まる、矩形disable
			}
		}

		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove_orbit : MonoBehaviour {
	// 円形～楕円軌道
	// 常に一定軌道を動く。
	// アクティブ時に弾撃つ。撃たない敵も用意(ただの的or邪魔奴)
	// 回転は一定なので
	
	EnemyBase enemybase;
	BulletBase bulletbase;
	Vector3 playerpos;
	Vector3 originpos;	// 判定とかの基準点：統一して動作させたいじゃん :)
	GameObject parent;

	public bool dependparent;	// 親オブジェ(空)用意してそれに合わせるときにｵｫﾝする
	public float rotatespd;		// 2自転速度：毎フレームに回す量。マイナスで逆方向
	public float period;		// 公転周期：単位は秒で。マイナスで逆方向
	public float rx;			// x軸方向の長さ
	public float ry;			// y軸方向の長さ。真円ならそろえる
	public float starttheta;	// 初期位相
	public float rotate;		// 軌道に角度つけるときに。
	private float theta;		// 回転角度計算用：時間経過で増えるゾ！
	private float x;
	private float y;
	private bool activate;		// 独自の索敵処理

	// 弾撃つときのやつ。基本的にcount1とおんなじ感じに。
	public bool shot;			// 弾撃つか否か
	public bool sourou;			// 非アクティブ時のランダムなctリセットをなくす
	public int bullet_ct1;
	public int bullet_ct2;
	public int blaze;
	public int way;
	public float wayint;
	private int bullet_ct;
	private int blazenow;

	void Start () {
		enemybase = GetComponent<EnemyBase>();
		bulletbase = GetComponent<BulletBase>();
		if (sourou == false) {bullet_ct = Random.Range(0, bullet_ct2);}
		theta = starttheta;
		if (dependparent == true) {
			originpos = transform.parent.transform.position;
		} else {
			originpos = transform.position;
		}
	}

	void Update () {
		if (bullet_ct > 0) {bullet_ct --;}
		// 動作関連
		// 自分の角度計算
		transform.Rotate(0,0,rotatespd);

		// ポジション計算
		x = originpos.x +  (rx * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Cos(rotate * Mathf.PI / 180f) - ry * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Sin(rotate * Mathf.PI / 180f));
		y = originpos.y + (rx * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Sin(rotate * Mathf.PI / 180f) + ry * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Cos(rotate * Mathf.PI / 180f));
		transform.position = new Vector3(x, y, 0);
		theta += 360f / (period * 60f);
	//	while (theta > 180f) {theta -= 360f;}
	//	while (theta < -180f) {theta += 360f;}

		// 索敵→アクティブ/非アクティブ判断
		playerpos = GameObject.Find("Player").transform.position;
		
		if ((playerpos - originpos).magnitude <= enemybase.search) {
			activate = true;
		} else {
			activate = false;
		}

		// 弾撃ち関連
		if (activate == false) {
			// 非アクティブ時の動作
			if (sourou == false) {bullet_ct = Random.Range(0, bullet_ct2);}
		} else if (activate == true && shot == true) {
			// 弾撃っちゃうよ
			if (bullet_ct <= 0) {
                blazenow ++;
                // 新弾発射処理(way考慮)
                Quaternion rot = Quaternion.Euler(0, 0, transform.localEulerAngles.z);
                // 奇数偶数分け
                if (way % 2 == 1) {
                    // 奇数
                    rot.eulerAngles -= new Vector3(0,0, wayint * (way / 2));
                } else {
                    // 偶数
                    rot.eulerAngles -= new Vector3(0,0, (way - 1)* wayint / 2);
                }
                // forループで角度足しつつShot
                for (int i = 1; i <= way; i++) {
                    bulletbase.Shot(transform.position, rot, bulletbase.image, 0, bulletbase.shotdist, true);
                    rot.eulerAngles += new Vector3(0,0,wayint);
                }
                    
                // ct設定
                if (blazenow == blaze) {
                    // any連射終了、ctを長い方(ct2)でセット
                    bullet_ct = bullet_ct2;
					blazenow = 0;
                } else {
                    bullet_ct = bullet_ct1;
                }

                
            }
		}
	}
}

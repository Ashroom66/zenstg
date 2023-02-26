using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rigidbody2Dコンポーネント必須にする
[RequireComponent(typeof(Rigidbody2D))]

public class BulletBase : MonoBehaviour {

	public Vector2 reangle;

	// 弾生成関連：弾画像、弾速、反動(主装備用)、発射位置調整用
	public GameObject image;
	public float speed;
	public float recoil;
	public float shotdist;

	public int dmg;	// 弾の攻撃力

	// 弾消滅関連：消滅モード+値
	public int delMode;
	public float delValue;
	int delcount;
	// 弾消滅mode1_一定距離
	public int delfirst;
	public Vector2 delorigin;
	public float distance;

	// その他：クールタイムとカウント用変数
	public int ct;
	public int ctCount;


	// 反動計算用
	public Vector3 rot3;

	// 処理

	// 弾生成、反動(自機側で呼び出す)
	public void Shot (Vector3 origin, Quaternion rotation, GameObject img,float recoile, float shotdistance, bool typeold = false) {
		// 角度計算
	//	float angle = rotation.z;
	//	reangle = new Vector2(Mathf.Cos((angle - 270) * Mathf.PI / 180), Mathf.Sin((angle - 270) * Mathf.PI / 180));
		// 生成
		// 直進は弾の方で設定

		// 発射位置調整(旧式、六角形とか)
		Vector3 shotorigin;
		if(typeold == true) {
			shotorigin = new Vector3 (origin.x + (Mathf.Cos((rotation.eulerAngles.z + 90) * Mathf.PI / 180) * shotdistance ),origin.y + (Mathf.Sin((rotation.eulerAngles.z + 90) * Mathf.PI / 180) * shotdistance ),0);
		} else {
			shotorigin = origin;
		}
		Instantiate(img, shotorigin, rotation);
		
		// 反動
		rot3 = new Vector3 (Mathf.Cos((rotation.eulerAngles.z + 90f) * Mathf.PI / 180),Mathf.Sin((rotation.eulerAngles.z + 90f) * Mathf.PI / 180),0f);
		GetComponent<Rigidbody2D>().AddForce(recoile * rot3.normalized　* -1 , ForceMode2D.Impulse);
	}

	// 消去(弾側、update内で呼び出す)
	public void Delete (int mode, float value) {
		if (mode == 0) {
			//	モード0：時間経過で消滅
			//	弾のdelValueにカウントダウンを重ね、0になったら消える
			delcount ++;
			if (delcount >= value) {
				Destroy (gameObject);
			}

		} else if (mode == 1) {
			// モード1：一定距離直進後消滅
			// 弾と発射地点の距離を求めて、delValue以上なら消す
			if (delfirst == 0) {
				delorigin = new Vector2 (transform.position.x, transform.position.y);
				delfirst ++;
			}
			distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - delorigin.x, 2) + Mathf.Pow(transform.position.y - delorigin.y, 2));
			if (distance >= value) {
				Destroy (gameObject);
			}
		}
	}

	public Quaternion diffusion(Quaternion moto, float accurate) {
		return Quaternion.Euler(0,0, moto.eulerAngles.z + Random.Range(-accurate, accurate));
	}
}

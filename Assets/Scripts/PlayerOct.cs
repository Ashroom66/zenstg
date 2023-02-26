using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOct : MonoBehaviour {

	// バレットベース呼び出す準備
	BulletBase bulletbase;
	
	// ステータス設定
	[System.Serializable]
	public class PlayerStatus {

	// 自機側設定：加速度、最高速度、体力
		public float acceleration;
		public float maxiV;
		public int hp;
		
	// 	弾設定：弾のPrefab、クールタイム
	//	public GameObject bullet;
	//	public int bulletCT;
	}


	// x,y：方向キー取得用
	// direction：移動方向
	float x;
	float y;
	Vector3 direction;
	// vは速度
	float v;

	[SerializeField] public PlayerStatus ps = new PlayerStatus();


	// 回避処理用
	// avoidance:回避力ぅ
	// avo_now:回避状態
	// avo_int:無敵時間の判定用
	// avo_interval:回避再使用までのインターバル
	// avo_count:カウント用
	// angle:角度取得用
	// force:回避方向と速度計算用
	public float avoidance;
	public bool avo_now;
	public int avo_int;
	public int avo_interval;
	int avo_count;
	public Vector3 angle;
	Vector2 force;


	// 弾用rotation
	public Quaternion bulletrot;


	void Start () {
		// バレットベース君取得
		bulletbase = GetComponent<BulletBase>();

		ps.acceleration = 0.1f;
		ps.maxiV = 10f;
		ps.hp = 10;
		//ps.bulletCT = 10;

		v = ps.maxiV / 60;

		// 回避初期設定
		avo_count = 0;
		avo_now = false;
	}
	

	void Update() {
		v = ps.maxiV / 60;

		// 移動
		
		/*
		まず、xとy変数にキー入力状況を取得させる。
		
		両方とも0：移動はしていない為減速。
			速度既に0ならそのままstay

		そうではない：移動。
			現在速度スカラーのv変数に加速度加える
			(もし超えた場合は逆に減らす)

		
		加速度について：主にUターン時とかの挙動考えてない為
						とりあえず速度だけで動かしてみる
			
		 */
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");

		// 移動する向きを決める
		direction = new Vector3 (x, y, 0).normalized;

		// 移動
		transform.position += direction * v;


		// 回避処理
		/*
		スペースキーで使用。
		自機の向いている方向へ向かって突撃

		後の追加点：	回避中は無敵
					　 回避中は弾を撃てない
			(回避中か否かを判定する変数を用意。それぞれの移動処理で呼び出し、
			T/Fで判断)

		インターバル：無敵or弾撃ちと再使用の差別化、別変数

		処理はaddForceのimpulseを用いる
		 */
		
		// インターバルカウント
		avo_count ++ ;

		// 念のためカウントが設定値を上回らないようにする
		if (avo_count > avo_interval) {
			avo_count = avo_interval;
		}

		// スペースキー受付→使用判定→使用
		if (Input.GetKey(KeyCode.Space) && avo_count >= avo_interval ) {
			// 回避処理
// 変更点：Octでは常にrotationZが0なのでLookAtOctより角度取得
			angle.z = (LookatOct.getangleOct() * (-1)) + 90;
			force = new Vector2(Mathf.Cos((angle.z - 270) * Mathf.PI / 180), Mathf.Sin((angle.z - 270) * Mathf.PI / 180));
			this.GetComponent<Rigidbody2D>().AddForce(force * avoidance, ForceMode2D.Impulse);

			// インターバルリセット
			// 回避中フラグON
			avo_count = 0;
			avo_now = true;

		} 

		// カウントが規定の無敵時間になったらフラグOFF
		if (avo_count == avo_int) {
			avo_now = false;
		}


		// 弾発射関連
		/*
		左クリックで使用。
		bulletbaceからある程度の変数用情報は読み込んだのでコンポーネント使用ゲー 
		*/
		bulletbase.ctCount ++;
		if (bulletbase.ctCount > bulletbase.ct) {
			bulletbase.ctCount = bulletbase.ct;
		}

		if (Input.GetMouseButton(0) && bulletbase.ctCount >= bulletbase.ct) {

// 変更点：Octでは常にrotationZが0なのでLookAtOctより角度取得、弾用rotに適用
			angle.z = (LookatOct.getangleOct() * (-1)) + 90;
			bulletrot = Quaternion.Euler(angle);

			// 弾生成
			// 原点は自機、角度も自機と揃える
			// 角度部分もちょい変更
			bulletbase.Shot(transform.position, bulletrot, bulletbase.image, bulletbase.recoil, bulletbase.shotdist);

			// カウントリセット
			bulletbase.ctCount = 0;
		}


	}
}

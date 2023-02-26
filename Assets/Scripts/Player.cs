using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// バレットベース呼び出す準備
	BulletBase bulletbase;
	
	// ステータス設定
	[System.Serializable]
	public class PlayerStatus {

	// 自機側設定：加速度、最高速度、体力
		public float acceleration;
		public float maxiV;
		public int maxihp;
		public int hp;
		
	// 	弾設定：弾のPrefab、クールタイム、火力ブースト
		public int atk;
		public int atk_boost;
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
	// hitct:被ダメ時クールタイム
	// point:強化pt
	public float avoidance;
	public bool avo_now;
	public int avo_int;
	public int avo_interval;
	public int avo_count;		// ゲージ表示の為
	Vector3 angle;
	Vector2 force;
	private int hitct;
	public int point;
	public int score;

	public bool canmove;	// 操作できるか否か

	public int nowwave;		// 現在のwave
	public int battletime;	// 合計戦闘時間(sec)


	SubEquip equipinfo;		// 被ダメ時ゲージ減らすのに使う
	Subcon subcon;
	public bool battlefin;	// 戦闘終了時にon→ダメ喰らわない

	string subname;

	AudioSource[] se;

	// 発射地点算出用ベクター君用意
	Vector3 shotorigin;
	Quaternion rot;

	// かすり
	int grazedmg;


	void Start () {
		// バレットベース君他取得
		bulletbase = GetComponent<BulletBase>();
		equipinfo = this.GetComponent<SubEquip>();
		subcon = this.GetComponent<Subcon>();
		se = GetComponents<AudioSource>();

		hitct = 0;


		ps.acceleration = 0.1f;
		//ps.bulletCT = 10;

		v = ps.maxiV / 60;

		// 回避初期設定
		avo_count = 0;
		avo_now = false;
		if (ps.maxihp < ps.hp) {ps.maxihp = ps.hp;}

	}
	

	void Update() {
		if (canmove == true) {
			v = ps.maxiV / 60;
			if (hitct > 0) {hitct --;}

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

			// 改良点：左利き用。hjkuでも移動できるようにする
			if(Input.GetKey(KeyCode.H)) {x = -1;}
			if(Input.GetKey(KeyCode.K)) {x = 1;}
			if(Input.GetKey(KeyCode.U)) {y = 1;}
			if(Input.GetKey(KeyCode.J)) {y = -1;}

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
				angle = transform.localEulerAngles;
				force = new Vector2(Mathf.Cos((angle.z - 270) * Mathf.PI / 180), Mathf.Sin((angle.z - 270) * Mathf.PI / 180));
				this.GetComponent<Rigidbody2D>().AddForce(force * avoidance, ForceMode2D.Impulse);
				se[3].Play();
				if (equipinfo.callweapon == "charge") {
					se[4].Play();
				}

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
				// 発射原点セット(Subcon流用)
				rot = Quaternion.Euler(0,0,transform.localEulerAngles.z);
				shotorigin = new Vector3 (transform.position.x + (Mathf.Cos((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),transform.position.y + (Mathf.Sin((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),0);
				// 弾生成
				// 原点は自機、角度も自機と揃える
				bulletbase.Shot(shotorigin, transform.rotation, bulletbase.image, bulletbase.recoil, bulletbase.shotdist);

				// カウントリセット
				bulletbase.ctCount = 0;
			}
		} 

		// 無敵時点滅
		if (hitct > 0) {
			float a = Mathf.Abs(Mathf.Sin(Time.time * 100));
			this.GetComponent<SpriteRenderer> ().color =  new Color(1f,1f,1f,a);
		} else {
			this.GetComponent<SpriteRenderer> ().color =  new Color(1f,1f,1f,1f);
		}

	}

	void OnTriggerEnter2D (Collider2D c) {
		if (battlefin == false) {
		if (hitct <= 0 &&(c.gameObject.tag == "EnemyBullet" || c.gameObject.tag == "Enemybody")&& avo_now == false) {
			hitct = 40;
			int hpdelta = ps.hp;
			se[2].Play();
			if (c.gameObject.tag == "EnemyBullet") {
				ps.hp -= c.GetComponent<BulletBase>().dmg;
			} else if (c.gameObject.tag == "Enemybody") {
				ps.hp -= c.GetComponent<EnemyBase>().atk;
			}
			hpdelta -= ps.hp;
			GameObject.Find("DamageUI").GetComponent<DamageUI>().DmgPopUp(transform.position, hpdelta);
			// 死ゾ
			if (ps.hp <= 0) {
				WaveEffector wavin = GameObject.Find("waveeffector").GetComponent<WaveEffector>();
				wavin.playerdied = true;
			}
			// 装備中のサブゲージ減らす。15%くらいがちょうどいい？
			if (equipinfo.nowequip == 1) {
				subname = equipinfo.subname1;
			} else if (equipinfo.nowequip == 2) {
				subname = equipinfo.subname2;
			} else if (equipinfo.nowequip == 3) {
				subname = equipinfo.subname3;
			} else if (equipinfo.nowequip == 4) {
				subname = equipinfo.subname4;
			} else if (equipinfo.nowequip == 5) {
				subname = equipinfo.subname5;
			}
			if (subname == "speed1") {				// 副装備追加時にはここも
				subcon.speed1.gauge -= (int)(subcon.speed1.cond3 * 0.15f);
			} else if (subname == "speed2") {
				subcon.speed2.gauge -= (int)(subcon.speed2.cond3 * 0.15f);
			} else if (subname == "big1") {
				subcon.big1.gauge -= (int)(subcon.big1.cond3 * 0.15f);
			} else if (subname == "big2") {
				subcon.big2.gauge -= (int)(subcon.big2.cond3 * 0.15f);
			} else if (subname == "way1") {
				subcon.way1.gauge -= (int)(subcon.way1.cond3 * 0.15f);
			} else if (subname == "way2") {
				subcon.way2.gauge -= (int)(subcon.way2.cond3 * 0.15f);
			} else if (subname == "pierce1") {
				subcon.pierce1.gauge -= (int)(subcon.pierce1.cond3 * 0.15f);
			} else if (subname == "pierce2") {
				subcon.pierce2.gauge -= (int)(subcon.pierce2.cond3 * 0.15f);
			} else if (subname == "homing1") {
				subcon.homing1.gauge -= (int)(subcon.homing1.cond3 * 0.15f);
			} else if (subname == "homing2") {
				subcon.homing2.gauge -= (int)(subcon.homing2.cond3 * 0.15f);
			} else if (subname == "charge") {
				subcon.charge.gauge -= (int)(subcon.charge.cond3 * 0.15f);
			} else if (subname == "hexashield") {
				subcon.hexashield.gauge -= (int)(subcon.hexashield.cond3 * 0.15f);
			}
		}
		// "かすり"の概念：
		if(hitct <= 0 &&(c.gameObject.tag == "EnemyBullet" || c.gameObject.tag == "Enemybody")&& avo_now == true && equipinfo.callweapon != "charge") {
			hitct = 4;
			int hpdelta = ps.hp;

			se[8].Play();
			if (c.gameObject.tag == "EnemyBullet") {
				grazedmg = (int)(c.GetComponent<BulletBase>().dmg * 0.2f);
			} else if (c.gameObject.tag == "Enemybody") {
				grazedmg = (int)(c.GetComponent<EnemyBase>().atk * 0.2f);
			}
			if(grazedmg == 0) {grazedmg ++;}
			if(GameObject.Find("つよいやつ")) {grazedmg = 0;}
			ps.hp -= grazedmg;
			hpdelta -= ps.hp;
			GameObject.Find("DamageUI").GetComponent<DamageUI>().DmgPopUp(transform.position, hpdelta);
			// 死ゾ
			if (ps.hp <= 0) {
				WaveEffector wavin = GameObject.Find("waveeffector").GetComponent<WaveEffector>();
				wavin.playerdied = true;
			}
		}
		}
	}
}

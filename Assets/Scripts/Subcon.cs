using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subcon : MonoBehaviour {
	// 安定のバレットベース君準備
	BulletBase bulletbase;


	// player.csのpsクラスのような要領でSubcon内にクラス作る。
	// 全Subconをこのスクリプトに作っていく
	// equip君の呼び出し文字列はこの中のSubタイプの名前でいける…と思う

	// ﾒﾝﾄﾞ

	// 使用変数名
	// i(wayループ用)
	// rot(way角度計算用)
	// subequip(副装備判定)
	private SubEquip subequip;

	// ガトリング用:プレイヤーの動作を一時的に制限する
	private Player player;
	private GameObject ui;	// 発射判定(メニューで選択時に暴発するのを防止する)



	// テンプレ君
	[System.Serializable]
	public class Sub {
		public int count;	// カウント用
		public int lvl;		// レベル

		// 各レベルで使うPrefab
		public GameObject lv1bullet;
		public GameObject lv2bullet;
		public GameObject lv3bullet;
		public GameObject lv3bullet2;	// hexashieldの二重軌道用

		// 各レベルのクールタイムタイム設定
		public int lv1ct;
		public int lv2ct;
		public int lv3ct;

		// 各レベルの反動設定
		public float lv1recoil;
		public float lv2recoil;
		public float lv3recoil;

		// 各レベルのバラつき設定
		public float lv1accurate;
		public float lv2accurate;
		public float lv3accurate;

		// (way弾用)各レベルのway数
		public int lv1way;
		public int lv2way;
		public int lv3way;

		// (way用)各レベルway弾間の角度のずれ"way interval"
		public float lv1wayint;
		public float lv2wayint;
		public float lv3wayint;
		
		// (speed2/gatling用)速射間隔と連射回数と最初のチャージ時間
		public int lv1charge;
		public int lv2charge;
		public int lv3charge;
		public int lv1braze;
		public int lv2braze;
		public int lv3braze;
		public int lv1ctsub;
		public int lv2ctsub;
		public int lv3ctsub;

		public int brazecount;	// 連射数カウント

		public int gauge;	// ゲージ
		public int cond2;	// レベル管理用(それぞれの値のげぇじ値でレベルアップ/ダウン)
		public int cond3;

	}
	public bool stay;	// stay時はこいつも制御不可に。主にQuotanによる制御

	[SerializeField] public Sub speed1 = new Sub();	// speed1
	[SerializeField] public Sub speed2 = new Sub();// Gatling
	[SerializeField] public Sub big1 = new Sub();	// big one
	[SerializeField] public Sub big2 = new Sub();	// cannon
	[SerializeField] public Sub way1 = new Sub();	// ウェイ1
	[SerializeField] public Sub way2 = new Sub();	// ShotGun
	[SerializeField] public Sub pierce1 = new Sub();// 貫通の1
	[SerializeField] public Sub pierce2 = new Sub();// お目目がフレア
	[SerializeField] public Sub homing1 = new Sub();// HOMO-ing1
	[SerializeField] public Sub homing2 = new Sub();// ホーミングミサイル
	[SerializeField] public Sub charge = new Sub();	// ヤシャスィーン
	[SerializeField] public Sub hexashield = new Sub();// TAMAWO FUSEGU

	// shot内で使うための"セルフ変数"的なやつ準備
	[SerializeField] public Sub s;

	AudioSource[] se;

	// 追加：クールタイム用。副装備追加時はここも
	private string[] subnames = {"speed1", "speed2", "big1", "big2", "way1", "way2", "pierce1", "pierce2", "homing1", "homing2", "charge", "hexashield"};
	private string nametook;

	// 発射地点算出用ベクター君用意
	Vector3 shotorigin;
	Quaternion rot;


	void Start () {
		// いつもの
		bulletbase = GetComponent<BulletBase>();
		subequip = GetComponent<SubEquip>();
		player = GameObject.Find("Player").GetComponent<Player>();
		ui = GameObject.Find("NormalUI");
		s = null;

		se = GetComponents<AudioSource>();
	}

	public void Shot (string name, int lvl) {
		// CT進める。アクティブなら2倍の速度で進む(旧システム流用)
		// レベルチェック&リターンもこっちに移行
		for(int i = 0; i < subnames.Length; i++) {
			nametook = subnames[i];
			if (nametook == "speed1") {s = speed1;}
			if (nametook == "speed2") {s = speed2;}
			if (nametook == "big1") {s = big1;}
			if (nametook == "big2") {s = big2;}
			if (nametook == "way1") {s = way1;}
			if (nametook == "way2") {s = way2;}
			if (nametook == "pierce1") {s = pierce1;}
			if (nametook == "pierce2") {s = pierce2;}
			if (nametook == "homing1") {s = homing1;}
			if (nametook == "homing2") {s = homing2;}
			if (nametook == "charge") {s = charge;}
			if (nametook == "hexashield") {s = hexashield;}
			if (s.count > 0) {s.count --;}
			if (s.gauge >= s.cond3) {
				s.gauge = s.cond3;
				s.lvl = 3;
			} else if (s.gauge >= s.cond2) {
				s.lvl = 2;
			} else {
				s.lvl = 1;
				if (s.gauge < 0) {s.gauge = 0;}
			}
			//Debug.Log("name:"+ nametook +", gauge:" + s.gauge + ", lvl:" + s.lvl);
			// ret lvl
			if (nametook == subequip.subname1) {
				subequip.substate1 = s.lvl;
			} else if (nametook == subequip.subname2) {
				subequip.substate2 = s.lvl;
			} else if (nametook == subequip.subname3) {
				subequip.substate3 = s.lvl;
			} else if (nametook == subequip.subname4) {
				subequip.substate4 = s.lvl;
			} else if (nametook == subequip.subname5) {
				subequip.substate5 = s.lvl;
			}
		}

		if (stay == false) {
		
		
		/*
		// Answer
		Sub s = null;

		if (name == "speed1") {
			s = speed1;
		}else if (name == "speed2") {
			s = speed2;
		}

		if(s == null) {
			Debug.LogError("name がまちがってます");
		}
		 */


		
		// 直進系
		if (name == "speed1" || name == "speed2" || name == "big1" || name == "big2" || name == "pierce1"　|| name == "pierce2") {
			// stringからclass呼び出す準備
			// 追加時はここにもelse ifでつなげていく
			if (name == "speed1") {
				s = speed1;
			} else if (name == "big1") {
				s = big1;
			} else if (name == "big2") {
				s = big2;
			} else if (name == "pierce1") {
				s = pierce1;
			} else if (name == "pierce2") {
				s = pierce2;
			} else if (name == "speed2") {
				s = speed2;
			} else {
				Debug.LogError("Nob "+ name + " is not exist");
			}
			// 処理
			// 基本形：プレイヤーの位置からまっすぐ弾撃つだけ
			// 上のif文に||で条件足せば簡単に他の基本形の副装処理できる

			// カウント進める
			if (s.count > 0) {
				s.count --;
			}

			// 弾発射
			// ガトリング発射処理(ガトリングだけは右クリに関係なく発射させる為)
			if (ui == null) {ui = GameObject.Find("NormalUI");}
			if (player == null) {player = GameObject.Find("Player").GetComponent<Player>();}
			if (player.canmove == false && s.count <= 0 && ui.activeSelf == true) {
				se[1].Stop();
				// 発射原点セット
				rot = Quaternion.Euler(0,0,transform.localEulerAngles.z);
				shotorigin = new Vector3 (transform.position.x + (Mathf.Cos((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),transform.position.y + (Mathf.Sin((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),0);
				// レベル分けした後、brazecountの値によって処理内容変える
				if (lvl == 1) {
					bulletbase.Shot(shotorigin, bulletbase.diffusion(transform.rotation, s.lv1accurate), s.lv1bullet, s.lv1recoil, bulletbase.shotdist);
					s.count = s.lv1ctsub;
					s.brazecount ++;
					// braze(連射階数)が達したらende
					if (s.brazecount >= s.lv1braze) {
						s.count = s.lv1ct;	// 普通の方のct設定
						player.canmove = true;
					}
				} else if (lvl == 2) {
					bulletbase.Shot(shotorigin, bulletbase.diffusion(transform.rotation, s.lv2accurate), s.lv2bullet, s.lv2recoil, bulletbase.shotdist);
					s.count = s.lv2ctsub;
					s.brazecount ++;
					// braze(連射階数)が達したらende
					if (s.brazecount >= s.lv2braze) {
						s.count = s.lv2ct;	// 普通の方のct設定
						player.canmove = true;
					}
				} else if (lvl == 3) {
					bulletbase.Shot(shotorigin, bulletbase.diffusion(transform.rotation, s.lv3accurate), s.lv3bullet, s.lv3recoil, bulletbase.shotdist);
					s.count = s.lv3ctsub;
					s.brazecount ++;
					// braze(連射階数)が達したらende
					if (s.brazecount >= s.lv3braze) {
						s.count = s.lv3ct;	// 普通の方のct設定
						player.canmove = true;
					}
				}
			}
			if (Input.GetMouseButton(1) && s.count <= 0) {
				// 発射原点セット
				rot = Quaternion.Euler(0,0,transform.localEulerAngles.z);
				shotorigin = new Vector3 (transform.position.x + (Mathf.Cos((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),transform.position.y + (Mathf.Sin((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),0);
				// レベルに応じて発射処理
				// 反動の関係で、ここでバラつきを考慮しておく
				if (name == "speed2" ) {
					// ガトリング処理
					if (player.canmove == true) {
						// 発射前動作：プレイヤーの動きを封じて発射準備
						player.canmove = false;
						s.brazecount = 0;
						se[1].Play();
						if (lvl == 1) {
							s.count = s.lv1charge;
						} else if (lvl == 2) {
							s.count = s.lv2charge;
						} else {
							s.count = s.lv3charge;
						}
					}
				} else {
					
					if (name == "big2") {se[6].Play();}
					if (lvl == 1) {
						bulletbase.Shot(shotorigin, bulletbase.diffusion(transform.rotation, s.lv1accurate), s.lv1bullet, s.lv1recoil, bulletbase.shotdist);
						s.count = s.lv1ct;
					} else if (lvl == 2) {
						bulletbase.Shot(shotorigin, bulletbase.diffusion(transform.rotation, s.lv2accurate), s.lv2bullet, s.lv2recoil, bulletbase.shotdist);
						s.count = s.lv2ct;
					} else if (lvl == 3) {
						bulletbase.Shot(shotorigin, bulletbase.diffusion(transform.rotation, s.lv3accurate), s.lv3bullet, s.lv3recoil, bulletbase.shotdist);
						s.count = s.lv3ct;
					}
				}
				
			}
			/*
			// レベルチェック&リターン
			if (s.gauge >= s.cond3) {
				s.gauge = s.cond3;
				s.lvl = 3;
			} else if (s.gauge >= s.cond2) {
				s.lvl = 2;
			} else {
				s.lvl = 1;
				if (s.gauge <= 0) {s.gauge = 0;}
			}
			// ret lvl
			if (name == subequip.subname1) {
				subequip.substate1 = s.lvl;
			} else if (name == subequip.subname2) {
				subequip.substate2 = s.lvl;
			} else if (name == subequip.subname3) {
				subequip.substate3 = s.lvl;
			} else if (name == subequip.subname4) {
				subequip.substate4 = s.lvl;
			} else if (name == subequip.subname5) {
				subequip.substate5 = s.lvl;
			}

			// s格納
			// ここにも追加していく
			if (name == "speed1") {
				speed1 = s;
			} else if (name == "speed2") {
				speed2 = s;
			} else if (name == "big1") {
				big1 = s;
			} else if (name == "big2") {
				big2 = s;
			} else if (name == "pierce1") {
				pierce1 = s;
			} else if (name == "pierce2") {
				pierce2 = s;
			}
			*/

		}

		// Shot処理つづき

		// way形
		if (name == "way1" || name == "way2" || name == "homing1" || name == "homing2" || name == "hexashield") {
			
			// stringからclass呼び出す準備
			// 追加時はここにもelse ifでつなげていく
			if (name == "way1") {
				s = way1;
			} else if (name == "way2") {
				s = way2;
			} else if (name == "homing1") {
				s = homing1;
			} else if (name == "homing2") {
				s = homing2;
			} else if (name == "hexashield") {
				s = hexashield;
			} else {
				Debug.LogError("Nob "+ name + " is not exist");	// Name Of Bullet
			}
			// 処理
			// way弾系：ループ使って角度ずらして発射するタイプ

			// カウント進める
			if (s.count > 0) {
				s.count --;
			}

			// 弾発射
			if (Input.GetMouseButton(1) && s.count <= 0) {
				if (name == "way2") {se[5].Play();}
				// レベルに応じて発射処理
				rot = Quaternion.Euler(0,0,transform.localEulerAngles.z);
				// 発射原点セット
				shotorigin = new Vector3 (transform.position.x + (Mathf.Cos((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),transform.position.y + (Mathf.Sin((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),0);
				if (lvl == 1) {
					// 最初に奇数/偶数分けて、初期の角度を計算
					if (s.lv1way % 2 == 1) {
						// 奇数：初期角度は2で割った切り捨ての数を引いたやつ
						rot.eulerAngles -= new Vector3(0,0, s.lv1wayint * (s.lv1way / 2));
						
					} else {
						// 偶数：初期角度は…式見ればわかる(思考放棄)
						rot.eulerAngles -= new Vector3(0,0,(s.lv1way - 1)* s.lv1wayint / 2);
					}
					// forループ：角度足しつつShot
					for(int i = 1;i <= s.lv1way; i ++) {
						bulletbase.Shot(shotorigin, bulletbase.diffusion(rot, s.lv1accurate), s.lv1bullet, s.lv1recoil, bulletbase.shotdist);
						rot.eulerAngles +=new Vector3(0,0, s.lv1wayint);
					}
					s.count = s.lv1ct;
				} else if (lvl == 2) {
					// 最初に奇数/偶数分けて、初期の角度を計算
					if (s.lv2way % 2 == 1) {
						// 奇数：初期角度は2で割った切り捨ての数を引いたやつ
						rot.eulerAngles -= new Vector3(0,0, s.lv2wayint * (s.lv2way / 2));
						
					} else {
						// 偶数：初期角度は…式見ればわかる(思考放棄)
						rot.eulerAngles -= new Vector3(0,0,(s.lv2way - 1)* s.lv2wayint / 2);
					}
					// forループ：角度足しつつShot
					for(int i = 1;i <= s.lv2way; i ++) {
						bulletbase.Shot(shotorigin, bulletbase.diffusion(rot, s.lv2accurate), s.lv2bullet, s.lv2recoil, bulletbase.shotdist);
						rot.eulerAngles +=new Vector3(0,0, s.lv2wayint);
					}
					s.count = s.lv2ct;
				} else if (lvl == 3) {
					// 最初に奇数/偶数分けて、初期の角度を計算
					if (s.lv3way % 2 == 1) {
						// 奇数：初期角度は2で割った切り捨ての数を引いたやつ
						rot.eulerAngles -= new Vector3(0,0, s.lv3wayint * (s.lv3way / 2));
						
					} else {
						// 偶数：初期角度は…式見ればわかる(思考放棄)
						rot.eulerAngles -= new Vector3(0,0,(s.lv3way - 1)* s.lv3wayint / 2);
					}
					// forループ：角度足しつつShot
					for(int i = 1;i <= s.lv3way; i ++) {
						bulletbase.Shot(shotorigin, bulletbase.diffusion(rot, s.lv3accurate), s.lv3bullet, s.lv3recoil, bulletbase.shotdist);
						if (name == "hexashield") {bulletbase.Shot(transform.position, bulletbase.diffusion(rot, s.lv3accurate), s.lv3bullet2, s.lv3recoil, bulletbase.shotdist);}	// シールド君lv3は外周も発射
						rot.eulerAngles +=new Vector3(0,0, s.lv3wayint);
					}
					s.count = s.lv3ct;
				}
			}
			/* 
			// レベルチェック&リターン
			if (s.gauge >= s.cond3) {
				s.gauge = s.cond3;
				s.lvl = 3;
			} else if (s.gauge >= s.cond2) {
				s.lvl = 2;
			} else {
				s.lvl = 1;
				if (s.gauge <= 0) {s.gauge = 0;}
			}
			// ret lvl
			if (name == subequip.subname1) {
				subequip.substate1 = s.lvl;
			} else if (name == subequip.subname2) {
				subequip.substate2 = s.lvl;
			} else if (name == subequip.subname3) {
				subequip.substate3 = s.lvl;
			} else if (name == subequip.subname4) {
				subequip.substate4 = s.lvl;
			} else if (name == subequip.subname5) {
				subequip.substate5 = s.lvl;
			}
			// s格納
			// ここにも追加していく
			if (name == "way1") {
				way1 = s;
			} else if (name == "way2") {
				way2 = s;
			} else if (name == "homing1") {
				homing1 = s;
			} else if (name == "homing2") {
				homing2 = s;
			} else if (name == "hexashield") {
				hexashield = s;
			}
			*/
		}


		// shot続き
		if (name == "charge") {
			
			// stringからclass呼び出す準備
			// 追加時はここにもelse ifでつなげていく
			if (name == "charge") {
				s = charge;
			} else {
				Debug.LogError("Nob "+ name + " is not exist");	// Name Of Bullet
			}
			// 処理

			// 突撃系は回避がonの時に自動的に使用。
			// カウント進める
			if (s.count > 0) {
				s.count --;
			}

			if (player.avo_now == true && s.count <= 0) {
				// 発射
				if (lvl == 1) {
					bulletbase.Shot(transform.position, bulletbase.diffusion(transform.rotation, s.lv1accurate), s.lv1bullet, s.lv1recoil, 0);
					s.count = s.lv1ct;
				} else if (lvl == 2) {
					bulletbase.Shot(transform.position, bulletbase.diffusion(transform.rotation, s.lv2accurate), s.lv2bullet, s.lv2recoil, 0);
					s.count = s.lv2ct;
				} else if (lvl == 3) {
					bulletbase.Shot(transform.position, bulletbase.diffusion(transform.rotation, s.lv3accurate), s.lv3bullet, s.lv3recoil, 0);
					s.count = s.lv3ct;
				}
			}

			
			/*
			// レベルチェック&リターン
			if (s.gauge >= s.cond3) {
				s.gauge = s.cond3;
				s.lvl = 3;
			} else if (s.gauge >= s.cond2) {
				s.lvl = 2;
			} else {
				s.lvl = 1;
				if (s.gauge <= 0) {s.gauge = 0;}
			}
			// ret lvl
			if (name == subequip.subname1) {
				subequip.substate1 = s.lvl;
			} else if (name == subequip.subname2) {
				subequip.substate2 = s.lvl;
			} else if (name == subequip.subname3) {
				subequip.substate3 = s.lvl;
			} else if (name == subequip.subname4) {
				subequip.substate4 = s.lvl;
			} else if (name == subequip.subname5) {
				subequip.substate5 = s.lvl;
			}
			// s格納
			// ここにも追加していく
			if (name == "charge") {
				charge = s;
			}
			*/
		}
		}
	}

}

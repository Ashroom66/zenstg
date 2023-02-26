using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class normalUI : MonoBehaviour {
	public bool isbattle;		// 戦闘時でないときはUpdateで副装備更新する為
	private GameObject player;
	private Player maininfo;
	private SubEquip subinfo;
	private RectTransform yellow;
	private Text lv1;
	private Text lv2;
	private Text lv3;
	private Text lv4;
	private Text lv5;
	private Text quotatext;
	private Text timetext;
	private Text scoretext;
	private Text pointtext;
	private Text wavetext;
	private Slider hpdisplay;
	private Slider avodisplay;
	private Timer timer;
	private Quotan quotan;
	private Slider bl1gauge;
	private Slider bl2gauge;
	private Slider bl3gauge;
	private Slider bl4gauge;
	private Slider bl5gauge;
	private Image ctmask1;
	private Image ctmask2;
	private Image ctmask3;
	private Image ctmask4;
	private Image ctmask5;
	private Subcon subcon;
	private int hp;
	private int hpmaxi;
	private int avo;
	private int avomaxi;
	private int time;
	private int score;
	private int points;
	private string s;		// テキスト表示用
	private int min;		// 時間表示の一時格納
	private float kijunvalue;	// ゲージ用
	private float nowvalue;		// ゲージ用
	private int loadcount;		// 装備ロードカウント：数フレーム待ってから読み込ませる

	// 地獄の副装備関連:詳しくはMenu_equipments
	string sub1;	// プレイヤー側の装備名
	string sub2;
	string sub3;
	string sub4;
	string sub5;
	bool s1 = false;
	bool s2 = false;
	bool s3 = false;
	bool s4 = false;
	bool s5 = false;
	GameObject bul1;
	GameObject bul2;
	GameObject bul3;
	GameObject bul4;
	GameObject bul5;
	GameObject emp1;
	GameObject emp2;
	GameObject emp3;
	GameObject emp4;
	GameObject emp5;
	RectTransform rec;
	Image img;

	private Subcon.Sub subconinf = null;	// 追加：ゲージ表示用
	


	void Subimage() {
		sub1 = subinfo.subname1;
		sub2 = subinfo.subname2;
		sub3 = subinfo.subname3;
		sub4 = subinfo.subname4;
		sub5 = subinfo.subname5;
		// 装備名有効判定：副装備追加時はここに条件追加していく
		if (sub1 == "speed1" || sub1 == "speed2" || sub1 == "big1" || sub1 == "big2" || sub1 == "way1" || sub1 == "way2" || sub1 == "pierce1" || sub1 == "pierce2" || sub1 == "homing1" || sub1 == "homing2" || sub1 == "charge" || sub1 == "hexashield") {
			s1 = true;
		} else {
			s1 = false;
		}
		if (sub2 == "speed1" || sub2 == "speed2" || sub2 == "big1" || sub2 == "big2" || sub2 == "way1" || sub2 == "way2" || sub2 == "pierce1" || sub2 == "pierce2" || sub2 == "homing1" || sub2 == "homing2" || sub2 == "charge" || sub2 == "hexashield") {
			s2 = true;
		} else {
			s2 = false;
		}
		if (sub3 == "speed1" || sub3 == "speed2" || sub3 == "big1" || sub3 == "big2" || sub3 == "way1" || sub3 == "way2" || sub3 == "pierce1" || sub3 == "pierce2" || sub3 == "homing1" || sub3 == "homing2" || sub3 == "charge" || sub3 == "hexashield") {
			s3 = true;
		} else {
			s3 = false;
		}
		if (sub4 == "speed1" || sub4 == "speed2" || sub4 == "big1" || sub4 == "big2" || sub4 == "way1" || sub4 == "way2" || sub4 == "pierce1" || sub4 == "pierce2" || sub4 == "homing1" || sub4 == "homing2" || sub4 == "charge" || sub4 == "hexashield") {
			s4 = true;
		} else {
			s4 = false;
		}
		if (sub5 == "speed1" || sub5 == "speed2" || sub5 == "big1" || sub5 == "big2" || sub5 == "way1" || sub5 == "way2" || sub5 == "pierce1" || sub5 == "pierce2" || sub5 == "homing1" || sub5 == "homing2" || sub5 == "charge" || sub5 == "hexashield") {
			s5 = true;
		} else {
			s5 = false;
		}
		if (s1 == true) {
			rec = bul1.GetComponent<RectTransform>();
			img = bul1.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub1);
			rec.position = new Vector3 (rec.position.x, 40, 0);
			rec = emp1.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		} else {
			// empty
			rec = emp1.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, 40, 0);
			rec = bul1.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		}
		if (s2 == true) {
			rec = bul2.GetComponent<RectTransform>();
			img = bul2.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub2);
			rec.position = new Vector3 (rec.position.x, 40, 0);
			rec = emp2.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		} else {
			// empty
			rec = emp2.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, 40, 0);
			rec = bul2.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		}
		if (s3 == true) {
			rec = bul3.GetComponent<RectTransform>();
			img = bul3.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub3);
			rec.position = new Vector3 (rec.position.x, 40, 0);
			rec = emp3.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		} else {
			// empty
			rec = emp3.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, 40, 0);
			rec = bul3.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		}
		if (s4 == true) {
			rec = bul4.GetComponent<RectTransform>();
			img = bul4.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub4);
			rec.position = new Vector3 (rec.position.x, 40, 0);
			rec = emp4.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		} else {
			// empty
			rec = emp4.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, 40, 0);
			rec = bul4.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		}
		if (s5 == true) {
			rec = bul5.GetComponent<RectTransform>();
			img = bul5.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub5);
			rec.position = new Vector3 (rec.position.x, 40, 0);
			rec = emp5.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		} else {
			// empty
			rec = emp5.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, 40, 0);
			rec = bul5.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -40, 0);
		}
	}

	float getsubctgauge(string subname) {
		// subcon流用。副装備追加時はここも
		if (subname == "speed1") {subconinf = subcon.speed1;}
		if (subname == "speed2") {subconinf = subcon.speed2;}
		if (subname == "big1") {subconinf = subcon.big1;}
		if (subname == "big2") {subconinf = subcon.big2;}
		if (subname == "way1") {subconinf = subcon.way1;}
		if (subname == "way2") {subconinf = subcon.way2;}
		if (subname == "pierce1") {subconinf = subcon.pierce1;}
		if (subname == "pierce2") {subconinf = subcon.pierce2;}
		if (subname == "homing1") {subconinf = subcon.homing1;}
		if (subname == "homing2") {subconinf = subcon.homing2;}
		if (subname == "charge") {subconinf = subcon.charge;}
		if (subname == "hexashield") {subconinf = subcon.hexashield;}
		if (subname == "") {return 1;}
		int now = subconinf.count;
		int kijun;
		if (subconinf.lvl == 1) {
			kijun = subconinf.lv1ct;
		} else if (subconinf.lvl == 2) {
			kijun = subconinf.lv2ct;
		} else {
			kijun = subconinf.lv3ct;
		}

		return (float)(now * 1.0 / kijun);

	}
	float getsublvlgauge(string subname) {
		// subcon流用。副装備追加時はここも
		if (subname == "speed1") {subconinf = subcon.speed1;}
		if (subname == "speed2") {subconinf = subcon.speed2;}
		if (subname == "big1") {subconinf = subcon.big1;}
		if (subname == "big2") {subconinf = subcon.big2;}
		if (subname == "way1") {subconinf = subcon.way1;}
		if (subname == "way2") {subconinf = subcon.way2;}
		if (subname == "pierce1") {subconinf = subcon.pierce1;}
		if (subname == "pierce2") {subconinf = subcon.pierce2;}
		if (subname == "homing1") {subconinf = subcon.homing1;}
		if (subname == "homing2") {subconinf = subcon.homing2;}
		if (subname == "charge") {subconinf = subcon.charge;}
		if (subname == "hexashield") {subconinf = subcon.hexashield;}
		if (subname == "") {return 0;}
		int now = subconinf.gauge;
		int kijun;
		if (subconinf.lvl == 1) {
			kijun = subconinf.cond2;
		} else if (subconinf.lvl == 2) {
			kijun = subconinf.cond3 - subconinf.cond2;
			now -= subconinf.cond2;
		} else {
			kijun = now;	// 満タン
		}

		return (float)(now * 1.0 / kijun);

	}

	void Start () {
		player = GameObject.Find("Player");
		maininfo = player.GetComponent<Player>();
		subinfo = player.GetComponent<SubEquip>();
		subcon = player.GetComponent<Subcon>();
		yellow = GameObject.Find("selectframe").GetComponent<RectTransform>();
		wavetext = GameObject.Find("moji_wave").GetComponent<Text>();
		lv1 = GameObject.Find("lv1").GetComponent<Text>();
		lv2 = GameObject.Find("lv2").GetComponent<Text>();
		lv3 = GameObject.Find("lv3").GetComponent<Text>();
		lv4 = GameObject.Find("lv4").GetComponent<Text>();
		lv5 = GameObject.Find("lv5").GetComponent<Text>();
		hpdisplay = GameObject.Find("hp_display").GetComponent<Slider>();
		avodisplay = GameObject.Find("avo_display").GetComponent<Slider>();
		quotatext = GameObject.Find("quotadisplay_value").GetComponent<Text>();
		timetext = GameObject.Find("timedisplay_value").GetComponent<Text>();
		pointtext = GameObject.Find("nUI_pointdisplay_value").GetComponent<Text>();
		scoretext = GameObject.Find("nUI_scoredisplay_value").GetComponent<Text>();
		bul1 = GameObject.Find("bl1");
		bul2 = GameObject.Find("bl2");
		bul3 = GameObject.Find("bl3");
		bul4 = GameObject.Find("bl4");
		bul5 = GameObject.Find("bl5");
		emp1 = GameObject.Find("em1");
		emp2 = GameObject.Find("em2");
		emp3 = GameObject.Find("em3");
		emp4 = GameObject.Find("em4");
		emp5 = GameObject.Find("em5");
		bl1gauge = GameObject.Find("bl1gauge").GetComponent<Slider>();
		bl2gauge = GameObject.Find("bl2gauge").GetComponent<Slider>();
		bl3gauge = GameObject.Find("bl3gauge").GetComponent<Slider>();
		bl4gauge = GameObject.Find("bl4gauge").GetComponent<Slider>();
		bl5gauge = GameObject.Find("bl5gauge").GetComponent<Slider>();
		ctmask1 = GameObject.Find("ctmask1").GetComponent<Image>();
		ctmask2 = GameObject.Find("ctmask2").GetComponent<Image>();
		ctmask3 = GameObject.Find("ctmask3").GetComponent<Image>();
		ctmask4 = GameObject.Find("ctmask4").GetComponent<Image>();
		ctmask5 = GameObject.Find("ctmask5").GetComponent<Image>();

		if (isbattle == true) {
			timer = GameObject.Find("Timer").GetComponent<Timer>();
			quotan = GameObject.Find("Quota").GetComponent<Quotan>();
		}
		
		Subimage();

		// WAVE〇〇はstart時でおｋ
		// save_loadのタイミング的に上手くいかなかったらupdateにもつけるかも。
		// 各waveでplayer君の設定そこだけ変えとけばいいかな？
		if (isbattle == true) {
			s = "STAGE" + maininfo.nowwave.ToString();
			wavetext.fontSize = 32;
		} else {
			// QKのUI:即ち試し撃ち
			s = "--";
			wavetext.fontSize = 24;
		}
		wavetext.text = s;

		subconinf = null;
	}

	void Update () {
		if (isbattle == false) {
			// QK部分なのでここも例のアレ追加
			Subimage();
		} else if (loadcount <5) {
			loadcount ++;
			if (loadcount == 5) {
				Subimage();	// 読み込み
				if (isbattle == true) {
					s = "STAGE" + maininfo.nowwave.ToString();
					wavetext.fontSize = 32;
				} else {
					// QKのUI:即ち試し撃ち
					s = "--";
					wavetext.fontSize = 24;
				}
				wavetext.text = s;
			}
		}

		//副装備選択フレーム部分
		if (subinfo.nowequip == 1) {
			yellow.position = new Vector3(-225 + 640, 40, 0);
		} else if (subinfo.nowequip == 2) {
			yellow.position = new Vector3(-150 + 640, 40, 0);
		} else if (subinfo.nowequip == 3) {
			yellow.position = new Vector3(-75 + 640, 40, 0);
		} else if (subinfo.nowequip == 4) {
			yellow.position = new Vector3(0 + 640, 40, 0);
		} else if (subinfo.nowequip == 5) {
			yellow.position = new Vector3(75 + 640, 40, 0);
		}

		// 副装備レベル部分
		if (s1 == true) {
			if (subinfo.substate1 == 1) {
				s = "Ⅰ";
			} else if (subinfo.substate1 == 2) {
				s = "Ⅱ";
			} else if (subinfo.substate1 == 3) {
				s = "Ⅲ";
			}
		} else {
			s = "";
		}
		lv1.text = s;
		if (s2 == true) {
			if (subinfo.substate2 == 1) {
				s = "Ⅰ";
			} else if (subinfo.substate2 == 2) {
				s = "Ⅱ";
			} else if (subinfo.substate2 == 3) {
				s = "Ⅲ";
			}
		} else {
			s = "";
		}
		lv2.text = s;
		if (s3 == true) {
			if (subinfo.substate3 == 1) {
				s = "Ⅰ";
			} else if (subinfo.substate3 == 2) {
				s = "Ⅱ";
			} else if (subinfo.substate3 == 3) {
				s = "Ⅲ";
			}
		} else {
			s = "";
		}
		lv3.text = s;
		if (s4 == true) {
			if (subinfo.substate4 == 1) {
				s = "Ⅰ";
			} else if (subinfo.substate4 == 2) {
				s = "Ⅱ";
			} else if (subinfo.substate4 == 3) {
				s = "Ⅲ";
			}
		} else {
			s = "";
		}
		lv4.text = s;
		if (s5 == true) {
			if (subinfo.substate5 == 1) {
				s = "Ⅰ";
			} else if (subinfo.substate5 == 2) {
				s = "Ⅱ";
			} else if (subinfo.substate5 == 3) {
				s = "Ⅲ";
			}
		} else {
			s = "";
		}
		lv5.text = s;

		// 副装備レベルゲージ
		/*
		// 旧式
		// 先に値求めてから代入先選択
		if (subinfo.nowequip != 0 && subinfo.callweapon != "") {	// 一応入れとく
				nowvalue = subcon.s.gauge;
			if (subcon.s.lvl == 1) {
				kijunvalue = subcon.s.cond2;
			} else if (subcon.s.lvl == 2) {
				kijunvalue = subcon.s.cond3 - subcon.s.cond2;
				nowvalue -= subcon.s.cond2;
			} else if (subcon.s.lvl == 3) {
				kijunvalue = nowvalue;	// 満タン
			} else {
				nowvalue = 0;	// すっからかん
				kijunvalue = 1;
			}
			if (subinfo.nowequip == 1) {
				bl1gauge.value = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 2) {
				bl2gauge.value = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 3) {
				bl3gauge.value = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 4) {
				bl4gauge.value = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 5) {
				bl5gauge.value = nowvalue / kijunvalue;
			}
		}
		*/
		// 新型
		bl1gauge.value = getsublvlgauge(subinfo.subname1);
		bl2gauge.value = getsublvlgauge(subinfo.subname2);
		bl3gauge.value = getsublvlgauge(subinfo.subname3);
		bl4gauge.value = getsublvlgauge(subinfo.subname4);
		bl5gauge.value = getsublvlgauge(subinfo.subname5);

			// 副装備CT表示
			/*
			// 旧式
			nowvalue = subcon.s.count;
			if (subcon.s.lvl == 1) {
				kijunvalue = subcon.s.lv1ct;
			} else if (subcon.s.lvl == 2) {
				kijunvalue = subcon.s.lv2ct;
			} else if (subcon.s.lvl == 3) {
				kijunvalue = subcon.s.lv3ct;
			} else {
				nowvalue = 1;	// すっからかん
				kijunvalue = 1;
			}
			if (subinfo.nowequip == 1) {
				ctmask1.fillAmount = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 2) {
				ctmask2.fillAmount = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 3) {
				ctmask3.fillAmount = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 4) {
				ctmask4.fillAmount = nowvalue / kijunvalue;
			} else if (subinfo.nowequip == 5) {
				ctmask5.fillAmount = nowvalue / kijunvalue;
			}
			 */

			// 新型
			ctmask1.fillAmount = getsubctgauge(subinfo.subname1);
			ctmask2.fillAmount = getsubctgauge(subinfo.subname2);
			ctmask3.fillAmount = getsubctgauge(subinfo.subname3);
			ctmask4.fillAmount = getsubctgauge(subinfo.subname4);
			ctmask5.fillAmount = getsubctgauge(subinfo.subname5);
		
		


		// HPと回避ゲージ部分
		hp = maininfo.ps.hp;
		hpmaxi = maininfo.ps.maxihp;
		avo = maininfo.avo_count;
		avomaxi = maininfo.avo_interval;
		hpdisplay.value = hp * 1f / hpmaxi;
		avodisplay.value = avo * 1f / avomaxi;

		// quota(ノルマ)と時間表示：isbattleでなければどちらも-
		if (isbattle == false) {
			s = "-";
		} else {
			// 戦闘時のquota表示
			s = quotan.childnum.ToString();
		}
		quotatext.text = s;

		if (isbattle == false) {
			s = "---:--";
		} else {
			min = 0;
			time = timer.sec;
			while (time >= 60) {
				min ++;
				time -= 60;
			}
			if (time < 10) {
				s = min.ToString() + ":0" + time.ToString();
			} else {
				s = min.ToString() + ":" + time.ToString();
			}
			
		}
		timetext.text = s;

		// スコアとポイント
		s = (maininfo.score).ToString();
		scoretext.text = s;
		s = (maininfo.point).ToString();
		pointtext.text = s;
	}
}

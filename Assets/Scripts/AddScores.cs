using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScores : MonoBehaviour {
	// スコアを加算する：敵に直接つけても良かったけど実績的なやつ作るかもしれないので一応
	// ついでにポイント加算する奴もつけた
	// そんな時間はないけどな！

	// 副装備のスコアもつけるょ
	Player player;
	Timer timer;
	Subcon sub;
	Subcon.Sub s;
	SubEquip equip;
	private int sec;
	private float bairitu;
	private float bonus;
	public int basesec;	// ボーナス加算基準時間(各WAVE毎に調整)
	public bool isbattle;	// バトル中はON

	// 追加：他装備も加算する為にループ使って云々
	private string[] subnames = {"speed1", "speed2", "big1", "big2", "way1", "way2", "pierce1", "pierce2", "homing1", "homing2", "charge", "hexashield"};

	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		if (isbattle == true) {
			timer = GameObject.Find("Timer").GetComponent<Timer>();
		}
		sub = GameObject.Find("Player").GetComponent<Subcon>();
	//	equip = GameObject.Find("Player").GetComponent<SubEquip>();
		
	}



	public void Add_score (int score, bool considertime, bool subexp, bool bulletonly, string subname = "") {
		// considertimeは時間に関係させるか否か。subexpは副装備にも経験値加えるか否か。
		bairitu = 1f;
		bonus = 0;

		if (considertime == true) {
			sec = timer.sec;
			bonus = (basesec - sec) * 1f / basesec;
		}
		if (bonus > 0) {bairitu += bonus;}

		if (bulletonly == false) {player.score += (int)(score * bairitu* bairitu);}	// 最大3倍
		if (subexp == true) {
			// 意外とめんどい副装備チェックｱｰﾝﾄﾞ加算
			/* 
			if (equip.nowequip == 1) {
				subname = equip.subname1;
			} else if (equip.nowequip == 2) {
				subname = equip.subname2;
			} else if (equip.nowequip == 3) {
				subname = equip.subname3;
			} else if (equip.nowequip == 4) {
				subname = equip.subname4;
			} else if (equip.nowequip == 5) {
				subname = equip.subname5;
			}*/
			if (subname.Contains("speed1")) {						// 副装備追加時はここにも
				sub.speed1.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("speed2")) {
				sub.speed2.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("big1")) {
				sub.big1.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("big2")) {
				sub.big2.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("way1")) {
				sub.way1.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("way2")) {
				sub.way2.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("homing2")) {
				sub.homing2.gauge += (int)(score * bairitu * 0.75f);		// homing2はpierce2より前
			} else if (subname.Contains("pierce1")) {
				sub.pierce1.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("pierce2")) {
				sub.pierce2.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("homing1")) {
				sub.homing1.gauge += (int)(score * bairitu * 0.75f);		// homing2は爆風とp2関係でpierce2より前に作る
			} else if (subname.Contains("charge")) {
				sub.charge.gauge += (int)(score * bairitu * 0.75f);
			} else if (subname.Contains("hexashield")) {
				sub.hexashield.gauge += (int)(score * bairitu * 0.75f);
			}

			// 追加：他装備も若干ゃ加算する。副装備追加時はここも。
			// subcon,　
			for(int i = 0; i < subnames.Length; i++) {
				string nametook = subnames[i];
				if (nametook == "speed1") {s = sub.speed1;}
				if (nametook == "speed2") {s = sub.speed2;}
				if (nametook == "big1") {s = sub.big1;}
				if (nametook == "big2") {s = sub.big2;}
				if (nametook == "way1") {s = sub.way1;}
				if (nametook == "way2") {s = sub.way2;}
				if (nametook == "pierce1") {s = sub.pierce1;}
				if (nametook == "pierce2") {s = sub.pierce2;}
				if (nametook == "homing1") {s = sub.homing1;}
				if (nametook == "homing2") {s = sub.homing2;}
				if (nametook == "charge") {s = sub.charge;}
				if (nametook == "hexashield") {s = sub.hexashield;}
				s.gauge += (int)(score * bairitu * 0.3f);
			}
		}


	}

	public void Add_point (int pt) {
		// こっちは時間考慮しないので。
		player.point += pt;
	}
}

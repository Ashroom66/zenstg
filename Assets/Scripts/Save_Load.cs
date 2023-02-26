using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Load : MonoBehaviour {
	// 主にシーン移動時にプレイヤーの情報を保存するやつ
	public static Player main;
	public static BulletBase bullet;
	public static SubEquip sub;

	// main情報
	public static float maxiV = 5.2f;
	public static int maxihp = 10;
	public static int hp = 10;
	public static int atk = 5;
	public static int atk_boost = 0;
	public static float avoidance = 10;
	public static int avo_int = 15;
	public static int avo_interval = 200;
	public static int point = 100;
	public static int score = 0;
	public static int nowwave = 0;
	public static int battletime = 0;

	// bullet情報
	public static float speed = 10;
	public static float recoil = 1;
	public static float shotdist = 0.4f;
	public static int dmg = 0;
	public static float delValue = 25;
	public static int ct = 40;
	
	// sub情報		nowequipとlvlは今回やらなくていいよね :)
	public static string subname1 = "";
	public static string subname2 = "";
	public static string subname3 = "";
	public static string subname4 = "";
	public static string subname5 = "";
	public static int substate1 = 0;
	public static int substate2 = 0;
	public static int substate3 = 0;
	public static int substate4 = 0;
	public static int substate5 = 0;

	// 副装備アンロック画面用
	public static int speed1 = -1;
	public static int speed2 = -1;
	public static int big1 = -1;
	public static int big2 = -1;
	public static int way1 = -1;
	public static int way2 = -1;
	public static int pierce1 = -1;
	public static int pierce2 = -1;
	public static int homing1 = -1;
	public static int homing2 = -1;
	public static int charge = -1;
	public static int hexashield = -1;
	public static Menu_subbutton button;

	/*
	void Start () {
		main = GameObject.Find("Player").GetComponent<Player>();
		bullet = GameObject.Find("Player").GetComponent<BulletBase>();
		sub = GameObject.Find("Player").GetComponent<SubEquip>();

	} */
	public static void Saveinfo() {
		main = GameObject.Find("Player").GetComponent<Player>();
		bullet = GameObject.Find("Player").GetComponent<BulletBase>();
		sub = GameObject.Find("Player").GetComponent<SubEquip>();
		// プレイヤー情報保存
		maxiV = main.ps.maxiV;
		maxihp = main.ps.maxihp;
		hp = main.ps.hp;
		atk = main.ps.atk;
		atk_boost = main.ps.atk_boost;
		avoidance = main.avoidance;
		avo_int = main.avo_int;
		avo_interval = main.avo_interval;
		point = main.point;
		score = main.score;
		nowwave = main.nowwave;
		battletime = main.battletime;
		// bullet情報
		speed = bullet.speed;
		recoil = bullet.recoil;
		shotdist = bullet.shotdist;
		dmg = bullet.dmg;
		delValue = bullet.delValue;
		ct = bullet.ct;
		// sub情報
		subname1 = sub.subname1;
		subname2 = sub.subname2;
		subname3 = sub.subname3;
		subname4 = sub.subname4;
		subname5 = sub.subname5;
		substate1 = sub.substate1;
		substate2 = sub.substate2;
		substate3 = sub.substate3;
		substate4 = sub.substate4;
		substate5 = sub.substate5;

	}

	public static void Loadinfo() {
		// プレイヤーの各種ステータスへ代入
		main = GameObject.Find("Player").GetComponent<Player>();
		bullet = GameObject.Find("Player").GetComponent<BulletBase>();
		sub = GameObject.Find("Player").GetComponent<SubEquip>();
		// プレイヤーへ情報代入
		main.ps.maxiV = maxiV;
		main.ps.maxihp = maxihp;
		main.ps.hp = hp;
		main.ps.atk = atk;
		main.ps.atk_boost = atk_boost;
		main.avoidance = avoidance;
		main.avo_int = avo_int;
		main.avo_interval = avo_interval;
		main.point = point;
		main.score = score;
		main.nowwave = nowwave;
		main.battletime = battletime;
		// bullet情報
		bullet.speed = speed;
		bullet.recoil = recoil;
		bullet.shotdist = shotdist;
		bullet.dmg = dmg;
		bullet.delValue = delValue;
		bullet.ct = ct;
		// sub情報
		sub.subname1 = subname1;
		sub.subname2 = subname2;
		sub.subname3 = subname3;
		sub.subname4 = subname4;
		sub.subname5 = subname5;
		sub.substate1 = substate1;
		sub.substate2 = substate2;
		sub.substate3 = substate3;
		sub.substate4 = substate4;
		sub.substate5 = substate5;
		// subレベルは全部1にする
		if (substate1 > 1) {sub.substate1 = 1;}
		if (substate2 > 1) {sub.substate2 = 1;}
		if (substate3 > 1) {sub.substate3 = 1;}
		if (substate4 > 1) {sub.substate4 = 1;}
		if (substate5 > 1) {sub.substate5 = 1;}
	}

	public static void Subload(string myname) {
		button = GameObject.Find(myname).GetComponent<Menu_subbutton>();
		if (myname == "speed1") {
			button.status = speed1;
		} else if (myname == "speed2") {
			button.status = speed2;
		} else if (myname == "big1") {
			button.status = big1;
		} else if (myname == "big2") {
			button.status = big2;
		} else if (myname == "way1") {
			button.status = way1;
		} else if (myname == "way2") {
			button.status = way2;
		} else if (myname == "pierce1") {
			button.status = pierce1;
		} else if (myname == "pierce2") {
			button.status = pierce2;
		} else if (myname == "homing1") {
			button.status = homing1;
		} else if (myname == "homing2") {
			button.status = homing2;
		} else if (myname == "charge") {
			button.status = charge;
		} else if (myname == "hexashield") {
			button.status = hexashield;
		}
	}

	public static void Subsave(string myname) {
		button = GameObject.Find(myname).GetComponent<Menu_subbutton>();
		if (myname == "speed1") {
			speed1 = button.status;
		} else if (myname == "speed2") {
			speed2 = button.status;
		} else if (myname == "big1") {
			big1 = button.status;
		} else if (myname == "big2") {
			big2 = button.status;
		} else if (myname == "way1") {
			way1 = button.status;
		} else if (myname == "way2") {
			way2 = button.status;
		} else if (myname == "pierce1") {
			pierce1 = button.status;
		} else if (myname == "pierce2") {
			pierce2 = button.status;
		} else if (myname == "homing1") {
			homing1 = button.status;
		} else if (myname == "homing2") {
			homing2 = button.status;
		} else if (myname == "charge") {
			charge = button.status;
		} else if (myname == "hexashield") {
			hexashield = button.status;
		}
	}

	public static void Resetinfo() {
		// main情報
	maxiV = 5.2f;
	maxihp = 10;
	hp = 10;
	atk = 5;
	atk_boost = 0;
	avoidance = 10;
	avo_int = 15;
	avo_interval = 200;
	point = 100;
	score = 0;
	nowwave = 0;
	battletime = 0;

	// bullet情報
	speed = 10;
	recoil = 1;
	shotdist = 0.4f;
	dmg = 0;
	delValue = 25;
	ct = 40;
	
	// sub情報		nowequipとlvlは今回やらなくていいよね :)
	subname1 = "";
	subname2 = "";
	subname3 = "";
	subname4 = "";
	subname5 = "";
	substate1 = 0;
	substate2 = 0;
	substate3 = 0;
	substate4 = 0;
	substate5 = 0;

	// 副装備アンロック画面用
	speed1 = -1;
	speed2 = -1;
	big1 = -1;
	big2 = -1;
	way1 = -1;
	way2 = -1;
	pierce1 = -1;
	pierce2 = -1;
	homing1 = -1;
	homing2 = -1;
	charge = -1;
	hexashield = -1;
	}

}

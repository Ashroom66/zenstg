using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_equipments : MonoBehaviour {
	// メニュー画面の副装備装備状態を管理する
	SubEquip player;
	string sub1;	// プレイヤー側の装備名
	string sub2;
	string sub3;
	string sub4;
	string sub5;
	bool s1 = false;	// 装備名が有効か否か
	bool s2 = false;	// 有効でないときにempty処理
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
	// 画像はsub1と統一している。Resourcesフォルダの中へlv3画像ﾎﾟｲ
	public void Start () {
		player = GameObject.Find("Player").GetComponent<SubEquip>();
		bul1 = GameObject.Find("bullet1");
		bul2 = GameObject.Find("bullet2");
		bul3 = GameObject.Find("bullet3");
		bul4 = GameObject.Find("bullet4");
		bul5 = GameObject.Find("bullet5");
		emp1 = GameObject.Find("empty1");
		emp2 = GameObject.Find("empty2");
		emp3 = GameObject.Find("empty3");
		emp4 = GameObject.Find("empty4");
		emp5 = GameObject.Find("empty5");
	}

	public void Update() {
		sub1 = player.subname1;
		sub2 = player.subname2;
		sub3 = player.subname3;
		sub4 = player.subname4;
		sub5 = player.subname5;
		
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
			rec.position = new Vector3 (rec.position.x, -660 + 720, 0);
			rec = emp1.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		} else {
			// empty
			rec = emp1.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -660 + 720, 0);
			rec = bul1.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		}
		if (s2 == true) {
			rec = bul2.GetComponent<RectTransform>();
			img = bul2.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub2);
			rec.position = new Vector3 (rec.position.x, -660 + 720, 0);
			rec = emp2.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		} else {
			// empty
			rec = emp2.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -660 + 720, 0);
			rec = bul2.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		}
		if (s3 == true) {
			rec = bul3.GetComponent<RectTransform>();
			img = bul3.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub3);
			rec.position = new Vector3 (rec.position.x, -660 + 720, 0);
			rec = emp3.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		} else {
			// empty
			rec = emp3.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -660 + 720, 0);
			rec = bul3.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		}
		if (s4 == true) {
			rec = bul4.GetComponent<RectTransform>();
			img = bul4.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub4);
			rec.position = new Vector3 (rec.position.x, -660 + 720, 0);
			rec = emp4.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		} else {
			// empty
			rec = emp4.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -660 + 720, 0);
			rec = bul4.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		}
		if (s5 == true) {
			rec = bul5.GetComponent<RectTransform>();
			img = bul5.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>(sub5);
			rec.position = new Vector3 (rec.position.x, -660 + 720, 0);
			rec = emp5.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		} else {
			// empty
			rec = emp5.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -660 + 720, 0);
			rec = bul5.GetComponent<RectTransform>();
			rec.position = new Vector3(rec.position.x, -800 + 720, 0);
		}
		
	}
}

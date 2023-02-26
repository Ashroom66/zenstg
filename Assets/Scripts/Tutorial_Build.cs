using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Build : MonoBehaviour {

	/*
	イージング：floatの配列で数値変化を表現。タイプで変化の具合を指定、モードで加減速具合を指定。

	例：30コマで座標A(x,y)から座標B(x2,y2)へ移動させたい
  		int deltaX[30], deltaY[30];
  		easing(30, deltaX, x, x2, (適当に));
  		easing(30, deltaY, x, x2, (適当に));
  
  		あとはforループでも使って1つずつ移動させる


	n:分割数(配列の要素数)。大きくするほど動き等が細かくなる
	type:イージングのタイプ。
  		0:Quadratic(二次関数的)
  		1:Cubic(三次関数的) 0のやつよりちょっとメリハリ大きい
  		2:Quartic(四次関数的) さらにメリハリ大きく
  		3:Quintic(五次関数的)
  		4:Exponential(指数関数的) 3のやつとあまり変わらないと思う

	mode(0-2):イージングモード
  		0:加減速両方
  		1:加速のみ
  		2:減速のみ
	*/
	public float[] easing(int n, int mode, int type, float start, float end) {
		int i;
		float d = end - start;
		float t;
		float[] a = new float[n];
		for(i = 0; i < n; i++) {
			t = (float)((i* 1.0) / (n*1.0));
    		if(type == 0) {
      			if(mode == 0) {
        			t *= 2;
        			if(t < 1) {
          				a[i] = (float)(d/2*t*t + start);
        			} else {
          				t--;
          				a[i] = (float)(-d/2 * (t*(t-2) - 1) + start);
        			}
      			} else if(mode == 1) {
        			a[i] = (float)(d*t*t + start);
      			} else {
        			a[i] = (float)(-d*t*(t-2) + start);
      			}
    		}
    		if(type == 1) {
      			if(mode == 0) {
        			t *= 2.0f;
        			if(t < 1) {
          				a[i] = (float)(d/2*t*t*t + start);
        			} else {
          				t -= 2;
          				a[i] = (float)(d/2 * (t*t*t + 2) + start);
        			}
      			} else if(mode == 1) {
        			a[i] = (float)(d*t*t*t + start);
      			} else {
        			t--;
        			a[i] = (float)(d*(t*t*t + 1) + start);
      			}
    		}
    		if(type == 2) {
      			if(mode == 0) {
        			t *= 2;
        			if(t < 1) {
          				a[i] = (float)(d/2*t*t*t*t + start);
        			} else {
          				t -= 2;
          				a[i] = (float)(-d/2 * (t*t*t*t - 2) + start);
        			}
      			} else if(mode == 1) {
        			a[i] = (float)(d*t*t*t*t + start);
      			} else {
        			t--;
        			a[i] = (float)(-d*(t*t*t*t - 1) + start);
      			}
    		}
    		if(type == 3) {
      			if(mode == 0) {
        			t *= 2;
        			if(t < 1) {
          				a[i] = (float)(d/2*t*t*t*t*t + start);
        			} else {
          				t-= 2;
          				a[i] = (float)(d/2 * (t*t*t*t*t + 2) + start);
        			}
      			} else if(mode == 1) {
        			a[i] = (float)(d*t*t*t*t*t + start);
      			} else {
        			t--;
        			a[i] = (float)(d*(t*t*t*t*t + 1) + start);
      			}
    		}
    		if(type == 4) {
      
      			if(mode == 0) {
        			t *= 2;
        			if(t < 1) {
          				a[i] = (float)(d/2 * Mathf.Pow(2.0f, 10*(t-1)) + start);
        			} else {
          				t--;
          				a[i] = (float)(d/2 * (-Mathf.Pow(2.0f, -10*t) + 2) + start);
        			}
      			} else if(mode == 1) {
        			t *= n;
        			a[i] = (float)(d * Mathf.Pow(2.0f, 10*(t/n - 1)) + start);
      			} else {
        			t *= n;
        			a[i] = (float)(d * (-Mathf.Pow(2.0f, -10*t/n) + 1) + start);
      			}
    		}
		}
		return a;
	}

	// Menu_Contrillerからの移植、tutorial用に改造
	// menu_Changeからも流用？(menu_change消す)
	// 最終的に通常のmenu_changeの機能も持たせる事！！
	private Player player;
	private GameObject menu;
	private GameObject ui;	// ノーマルui
	private int count = 0;	// カウント用
	private int step = 0;

	// menu_modechange
	private bool isMain;
	private GameObject main;
	private GameObject sub;
	AudioSource se;

	public RectTransform main1;
	public RectTransform main2;
	public RectTransform main3;
	public RectTransform sub1;
	public RectTransform sub2;
	public RectTransform sub2b;
	public RectTransform sub3;
	public RectTransform sub4;
	public Image tutorial_blackout;
	public RectTransform tutorial_blackout_rect;
	public GameObject menu_changer;
	public GameObject menu_controller;
	private float[] ease1;
	private float[] ease2;
	private float[] ease3;
	private float[] ease4;
	private Vector2 rectsize;
	public  int easetype;

	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		menu = GameObject.Find("Menu");
		ui = GameObject.Find("NormalUI");
		main = GameObject.Find("Main");
		sub = GameObject.Find("Sub");
		//menu_controller = GameObject.Find("MenuController");
		//menu_changer = GameObject.Find("MenuModeChanger");
		main1 = GameObject.Find("tutorial_main1").GetComponent<RectTransform>();
		main2 = GameObject.Find("tutorial_main2").GetComponent<RectTransform>();
		main3 = GameObject.Find("tutorial_main3").GetComponent<RectTransform>();
		sub1 = GameObject.Find("tutorial_sub1").GetComponent<RectTransform>();
		sub2 = GameObject.Find("tutorial_sub2").GetComponent<RectTransform>();
		sub2b = GameObject.Find("tutorial_sub2b").GetComponent<RectTransform>();
		sub3 = GameObject.Find("tutorial_sub3").GetComponent<RectTransform>();
		sub4 = GameObject.Find("tutorial_sub4").GetComponent<RectTransform>();
		tutorial_blackout = GameObject.Find("tutorial_blackout").GetComponent<Image>();
		tutorial_blackout_rect = GameObject.Find("tutorial_blackout").GetComponent<RectTransform>();
		se = GetComponent<AudioSource>();
		isMain = true;
		menu.SetActive(true);
		ui.SetActive(false);
		sub.SetActive(false);
		player.canmove = false;				// 行動を封じる
		Save_Load.Loadinfo();
		player.ps.hp = player.ps.maxihp;	// 体力全回復(適当)

		rectsize = main1.sizeDelta;
		rectsize.y = 0;
		main1.sizeDelta = rectsize;
	}

	void Update() {
		if (count <= 30) {count ++;}
		if(step >= 50) { // step50以上で通常メニュー解禁(50,というよりデカい数　チュートリアル終わったら)
			// Tabキー(もっと良さげなキーあったら後々変更)でメニューON/OFF切り替え
			if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Return)) && count > 30) {
				if (player.canmove == true) {
					// メニュー展開
					menu.SetActive(true);
					ui.SetActive(false);
					player.canmove = false;
				} else {
					// メニュー非アクティブ
					menu.SetActive(false);
					ui.SetActive(true);
					player.canmove = true;
				}
			}
			// menu_change
			if (isMain == true && ( Input.GetAxisRaw("Horizontal") > 0  || Input.GetKeyDown(KeyCode.K)) ) {
				sub.SetActive(true);
				main.SetActive(false);
				isMain = false;
				se.Play();
			} else if (isMain == false && (Input.GetAxisRaw("Horizontal") < 0  || Input.GetKeyDown(KeyCode.H)) ) {
				main.SetActive(true);
				sub.SetActive(false);
				isMain = true;
				se.Play();
			}
		}
		
		// step移行関連
		if(Input.GetMouseButtonDown(0) && count > 10) {
			// 少なくとも10f以上で動作させるので…　メニュー切り替えの特別なステップ以外はコレでまとめる。
			if(step == 0 && count > 30) {
				// st0→1
				main1.localPosition = new Vector3(495, 290, 0);
				rectsize = main1.sizeDelta;
				ease1 = easing(11, 2, easetype, 0.6f, 60);
				count = 0;
				step ++;
			} else if(step == 1) {
				main2.localPosition = new Vector3(0,-40, 0);
				ease1 = easing(11, 1, easetype, 60, 0.6f);
				ease2 = easing(11, 0, easetype, 0.6f, 600);
				count = 0;
				step ++;
			} else if(step == 2) {
				// st2→3
				ease1 = easing(11, 0, easetype, 600, 0.6f);
				count = 0;
				step ++;
			} else if(step == 4) {
				sub2.localPosition = new Vector3(-540, 180, 0);
				sub2b.localPosition = new Vector3(0, -150, 0);
				ease1 = easing(11, 2, easetype, 0.6f, 100);
				ease2 = easing(11, 2, easetype, 0.6f, 200);
				count = 0;
				step ++;
			} else if(step == 5) {	// 本来なら上に薄いボタン重ねて云々
				sub3.localPosition = new Vector3(300, -295, 0);
				ease1 = easing(11, 2, easetype, 0.6f, 100);
				count = 0;
				step ++;
			} else if(step == 6) { // 本来ならアンロックボタンで作動
				// 1～5キーの表示
				sub4.localPosition = new Vector3(-340, -300, 0);
				ease1 = easing(11, 1, easetype, 60, 0.6f);
				ease2 = easing(11, 1, easetype, 100, 0.6f);
				ease3 = easing(11, 1, easetype, 200, 0.6f);
				ease4 = easing(11, 2, easetype, 0.6f, 80);
				count = 0;
				step ++;
			} else if(step == 7) {
				// 7→8, 1～5ハイライト消去、AH←キーの入力待ち
				ease1 = easing(11, 1, easetype, 80, 0.6f);
				count = 0;
				step ++;
			} else if(step ==9) {
				count = 0;
				step ++;
			} else if(step == 10) {
				main3.localPosition = new Vector3(180, 305, 0);
				ease1 = easing(11, 2, easetype, 0.6f, 80);
				count = 0;
				step ++;
			} else if(step == 11) {
				// 消去の流れ
				ease1 = easing(21, 0, easetype, 730, 0.6f);
				ease2 = easing(21, 1, easetype, 0.8f, 0.2f);
				count = 0;
				step ++;
			}
		}

		if(step == 3 && count > 10 && (Input.GetAxisRaw("Horizontal") > 0  || Input.GetKeyDown(KeyCode.K)) ) {
			// main→subの動き
			sub.SetActive(true);
			main.SetActive(false);
			isMain = false;
			sub1.localPosition = new Vector3(495, 290, 0);
			ease1 = easing(11, 2, easetype, 0.6f, 60);
			step ++;
			// se.Play();
		}
		if(step == 8 && count > 10 && (Input.GetAxisRaw("Horizontal") < 0  || Input.GetKeyDown(KeyCode.H)) ) {
			// sub→mainの動き
			sub.SetActive(false);
			main.SetActive(true);
			isMain = true;
			step ++;
			// se.Play();
		}

		// step非移行、表示部分変更関連(多分図形の拡大縮小のみに使用)
		if(count <= 10) {
			if(step == 1) {
				rectsize.y = ease1[count];
				main1.sizeDelta = rectsize;
			}
			if(step == 2) {
				main1.sizeDelta = new Vector2(main1.sizeDelta.x, ease1[count]);
				if(count == 10) {main1.localPosition = new Vector3(1495, 290, 0);}
				main2.sizeDelta = new Vector2(main2.sizeDelta.x, ease2[count]);
			}
			if(step == 3) {
				main2.sizeDelta = new Vector2(main2.sizeDelta.x, ease1[count]);
				if(count == 10) {main2.localPosition = new Vector3(0, -1050, 0);}
			}
			if(step == 4) {
				// main -> sub
				sub1.sizeDelta = new Vector2(sub1.sizeDelta.x, ease1[count]);
			}
			if(step == 5) {
				sub2.sizeDelta = new Vector2(sub2.sizeDelta.x, ease1[count]);
				sub2b.sizeDelta = new Vector2(sub2b.sizeDelta.x, ease2[count]);
			}
			if(step == 6) {
				sub3.sizeDelta = new Vector2(sub3.sizeDelta.x, ease1[count]);
			}
			if(step == 7) {
				sub1.sizeDelta = new Vector2(sub1.sizeDelta.x, ease1[count]);
				sub2.sizeDelta = new Vector2(sub2.sizeDelta.x, ease2[count]);
				sub2b.sizeDelta = new Vector2(sub2b.sizeDelta.x, ease3[count]);
				sub3.sizeDelta = new Vector2(sub3.sizeDelta.x, ease2[count]);
				sub4.sizeDelta = new Vector2(sub4.sizeDelta.x, ease4[count]);
				if(count == 10) {
					sub1.localPosition = new Vector3(1495, 290, 0);
					sub2.localPosition = new Vector3(-1540, 180, 0);
					sub2b.localPosition = new Vector3(0, -1150, 0);
					sub3.localPosition = new Vector3(300, -1295, 0);
				}
			}
			if(step == 8) {
				sub4.sizeDelta = new Vector2(sub4.sizeDelta.x, ease1[count]);
				if(count == 10) {sub4.localPosition = new Vector3(-340, -1300, 0);}
			}
			if(step == 11) {
				main3.sizeDelta = new Vector2(main3.sizeDelta.x, ease1[count]);
			}
		}
		if(count <= 20 && step == 12) {
			tutorial_blackout_rect.sizeDelta = new Vector2(tutorial_blackout_rect.sizeDelta.x, ease1[count]);
			tutorial_blackout.color = new Color(0,0,0, ease2[count]);
			if(count == 20) {
				tutorial_blackout_rect.localPosition = new Vector3(0, 1000, 0);
				step = 100;
				menu_changer.SetActive(true);
				menu_controller.SetActive(true);
				Destroy(this.gameObject);
			}
		}
	}

	// add other functions

	
}

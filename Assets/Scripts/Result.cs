using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class Result : MonoBehaviour {
	// リザルト表示兼ハイスコアチェック+書き込みとか。
	// ハイスコア書き込みファイルはlevel8;
	StreamReader sr;
	StreamWriter sw;
	FileInfo fi;
	Player player;
	Subcon subcon;
	int texthighscore;
	int nowscore;
	int b_timeinfo;		// 戦闘時間=info
	int b_timeinfomin;	// 分情報
	bool ishighscore;
	Text score;
	Text stage;			// 最高到達ステージ(乙った箇所orクリア)
	Text battletime;
	Text ty;			// ハイスコア更新時にちょっとコメント変えたり。
	GameObject blackout;
	Image blackimage;
	float a = 1f;
	bool deleted;



	void Start () {
		// ハイスコアチェック
		player = GameObject.Find("Player").GetComponent<Player>();
		subcon = GameObject.Find("Player").GetComponent<Subcon>();
		score = GameObject.Find("score").GetComponent<Text>();
		stage = GameObject.Find("stage").GetComponent<Text>();
		ty = GameObject.Find("ty :)").GetComponent<Text>();
		blackout = GameObject.Find("blackout");
		blackimage = blackout.GetComponent<Image>();
		battletime = GameObject.Find("battletime").GetComponent<Text>();
		/*
		fi = new FileInfo(Application.dataPath + "/" + "highscore.info");

		if(!File.Exists(Application.dataPath + "/" + "highscore.info")) {
			Debug.Log("null");
			 File.WriteAllText(Application.dataPath + "/" + "highscore.info", "0");
		}

		using (sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)) {
			texthighscore = Convert.ToInt32(sr.ReadToEnd());
		}
		*/
		texthighscore = 999999999;
		Save_Load.Loadinfo();
		player.canmove = false;
		subcon.stay = true;
		nowscore = player.score;
		if (nowscore > texthighscore) {
			ishighscore = true;
		//	sw = fi.AppendText();
			sw = new StreamWriter(Application.dataPath + "/" + "highscore.info", false); 
			sw.WriteLine(nowscore.ToString());
			sw.Flush();
			sw.Close();
		} else {
			ishighscore = false;
		}

		// 表示
		if (player.nowwave == 114514) {
			stage.text = "到達stage： 5(CLEAR)";
		} else if (player.nowwave <= 5) {
			stage.text = "到達stage： " + player.nowwave.ToString();
		}
		// 時間
		b_timeinfo = player.battletime;
		b_timeinfomin = 0;
		while (b_timeinfo >= 60) {
			b_timeinfomin ++;
			b_timeinfo -= 60;
		}
		battletime.text = "合計戦闘時間： " + b_timeinfomin.ToString() + "分" + b_timeinfo.ToString() + "秒";
		// スコア
		if (ishighscore == true) {
			score.color = new Color(1f, 1f, 0.7f, 1f);
			score.text = "スコア： " + nowscore.ToString() + "(ハイスコア更新！)";
		} else {
			score.text = "スコア： " + nowscore.ToString();
		}
		// ty
		if (ishighscore == true) {
			ty.text = "Ty for playing :D";
		} else {
			ty.text = "Ty for playing :)";
		}

		deleted = false;
	}

	void Update (){
		// ブラックアウト君を10fくらいかけて消えさせる
		if (a > 0) {
			a -= 0.1f;
			blackimage.color = new Color(0,0,0,a);
		} else {
			if (deleted == false) {
				Destroy(blackout.gameObject);
				deleted = true;
			}
			

		}
	}

}

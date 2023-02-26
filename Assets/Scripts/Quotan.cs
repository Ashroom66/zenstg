using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quotan : MonoBehaviour {
	// いわゆるノルマ
	// 多分ここからシーン遷移とかやる
	// 最初の「WAVE〇〇 START!!」的なものもここでやるかも？

	public int childnum;
	private Timer timer;
	private Player player;
	private Subcon subcon;
	private WaveEffector waveeffector;
	private int count = 0;
	AudioSource bgm;
	float startvol;


	void Start () {
		timer = GameObject.Find("Timer").GetComponent<Timer>();
		player = GameObject.Find("Player").GetComponent<Player>();
		subcon = GameObject.Find("Player").GetComponent<Subcon>();
		waveeffector = GameObject.Find("waveeffector").GetComponent<WaveEffector>();
		bgm = GetComponent<AudioSource>();

		Save_Load.Loadinfo();
	}

	void Update () {
		// wave開始処理
		if (count < 120) {
			waveeffector.WaveStart(count);
			subcon.stay = true;
			player.canmove = false;
			count ++;
		} else if (count == 120) {
			if (childnum > 0) {
				timer.active = true;
				subcon.stay = false;
				player.canmove = true;
				count = 200;
				bgm.Play();
				startvol = bgm.volume;
			}
		}

		childnum = this.transform.childCount;
		if (childnum == 0) {
			if (count < 320) {
				waveeffector.WaveFinish(count - 200);
				bgm.volume = startvol * (320 - count) * 1f / 120f;
				subcon.stay = true;
				player.canmove = false;
				player.battlefin = true;
				count ++;
			} else {
				// 終了 + シーン遷移
				timer.active = false;
				subcon.stay = false;
				player.battlefin = false;
				player.battletime += timer.sec;
				Save_Load.Saveinfo();
				if (player.nowwave == 5) {
					// ゲームクリア
					player.nowwave = 114514;
					Save_Load.Saveinfo();
					SceneManager.LoadScene("result");
				} else {
					SceneManager.LoadScene("Proto_QK");
				}
				
			}
			

		}
	}
}

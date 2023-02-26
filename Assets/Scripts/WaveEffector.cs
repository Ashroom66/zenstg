using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveEffector : MonoBehaviour {
	// 呼び出されたら指定されたvoid関数に応じてwave関係の文字バーってやったり色々やる

	public RectTransform wave;
	public RectTransform start;
	public RectTransform finish;
	public Text wavetext;
	public Image blackout;
	public float a;		// ブラックアウト君の透明度指定
	public float wx1;	// 動作時wave始点
	public float wx2;	// 終点
	public float anyx1;
	public float anyx2;
	public float x;		// 計算用
	public float hoseix = 640;
	public float hoseiy = 360;
	// ゲームオーバー処理
	private int count = 0;
	public bool playerdied = false;
	Player player;
	Subcon subcon;
	AudioSource bgm;
	float startvol;
	Timer timer;


	public void Start () {
		wave = GameObject.Find("waveeffect_wave").GetComponent<RectTransform>();
		start = GameObject.Find("waveeffect_start").GetComponent<RectTransform>();
		finish = GameObject.Find("waveeffect_finish").GetComponent<RectTransform>();
		blackout = GameObject.Find("blackout").GetComponent<Image>();
		wavetext = GameObject.Find("waveeffect_wave").GetComponent<Text>();
	}


	public void WaveStart(int time) {
		// 120fかけて最初の処理やる
		
		if (time <= 10) {
			wx1 = -880;
			wx2 = -15;
			anyx1 = 880;
			anyx2 = 15;
			// 動作
			x = ((10-time) * wx1 + time * wx2) / 10f;
			wave.position = new Vector3(x + hoseix, 70 + hoseiy, 0);
			x = ((10-time) * anyx1 + time * anyx2) / 10f;
			start.position = new Vector3(x + hoseix, -70 + hoseiy, 0);
		} else if (time <= 80) {
			wx1 = -15;
			wx2 = 15;
			anyx1 = 15;
			anyx2 = -15;
			// 動作
			x = ((80-time) * wx1 + (time-10) * wx2) / 30f;
			wave.position = new Vector3(x + hoseix, 70 + hoseiy, 0);
			x = ((80-time) * anyx1 + (time-10) * anyx2) / 30f;
			start.position = new Vector3(x + hoseix, -70 + hoseiy, 0);
		} else if (time <= 90) {
			wx1 = 15;
			wx2 = 880;
			anyx1 = -15;
			anyx2 = -880;
			// 動作
			x = ((90-time) * wx1 + (time-80) * wx2) / 10f;
			wave.position = new Vector3(x + hoseix, 70 + hoseiy, 0);
			x = ((90-time) * anyx1 + (time-80) * anyx2) / 10f;
			start.position = new Vector3(x + hoseix, -70 + hoseiy, 0);
		} else {
			a = (time - 89)*1f / 30f;
			blackout.color = new Color(0, 0, 0, 1-a);
		}
	}

	public void WaveFinish(int time) {
		// 120fかけて終わりの処理やる
		if (time == 0) {wavetext.color = new Color(32f/255f,32f/255f,32f/255f,1);}	// 黒に塗りつぶす
		if (time <= 10) {
			wx1 = -880;
			wx2 = -15;
			anyx1 = 880;
			anyx2 = 15;
			// 動作
			x = ((10-time) * wx1 + time * wx2) / 10f;
			wave.position = new Vector3(x + hoseix, 70 + hoseiy, 0);
			x = ((10-time) * anyx1 + time * anyx2) / 10f;
			finish.position = new Vector3(x + hoseix, -70 + hoseiy, 0);
		} else if (time <= 80) {
			wx1 = -15;
			wx2 = 15;
			anyx1 = 15;
			anyx2 = -15;
			// 動作
			x = ((80-time) * wx1 + (time-10) * wx2) / 30f;
			wave.position = new Vector3(x + hoseix, 70 + hoseiy, 0);
			x = ((80-time) * anyx1 + (time-10) * anyx2) / 30f;
			finish.position = new Vector3(x + hoseix, -70 + hoseiy, 0);
		} else if (time <= 90) {
			wx1 = 15;
			wx2 = 880;
			anyx1 = -15;
			anyx2 = -880;
			// 動作
			x = ((90-time) * wx1 + (time-80) * wx2) / 10f;
			wave.position = new Vector3(x + hoseix, 70 + hoseiy, 0);
			x = ((90-time) * anyx1 + (time-80) * anyx2) / 10f;
			finish.position = new Vector3(x + hoseix, -70 + hoseiy, 0);
		} else {
			a = (time - 90)*1f / 30f;
			blackout.color = new Color(0, 0, 0, a);
		}
	}

	void Update () {
		if (playerdied == true) {
			if (count == 0) {
				// プレイヤー情報読み込み
				player = GameObject.Find("Player").GetComponent<Player>();
				subcon = GameObject.Find("Player").GetComponent<Subcon>();
				// タイマー加算
				timer = GameObject.Find("Timer").GetComponent<Timer>();
				player.battletime += timer.sec;
				Save_Load.Saveinfo();
				// 音停止
				bgm = GameObject.Find("Quota").GetComponent<AudioSource>();
				startvol = bgm.volume;
			} else if (count < 60) {
				// 待機
				bgm.volume = startvol * (60 - count) * 1f/60f;
			} else if (count < 150) {
				// 90fかけてフェードアウト
				a = (count - 60f) / 90f;
				blackout.color = new Color(0, 0, 0, a);
			} else {
				// リザルト

				SceneManager.LoadScene("result");
			}
			player.canmove = false;
			subcon.stay = true;
			count ++;
		}
	}
	
}

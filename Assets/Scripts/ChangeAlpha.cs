using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAlpha : MonoBehaviour {
	// 一定周期で設定した透明度間を反復させる

	public float min_alpha;	// 透明度低い方(=要するにはっきり見える方)
	public float max_alpha;	// 0～100想定
	public int period;		// 周期(/flame)
	public float r;
	public float g;
	public float b;

	private float mina;
	private float maxa;
	private int now;		// カウント用
	private float nowa;
	private Image img;
	void Start() {
		mina = 1f - (1f * min_alpha / 100);
		maxa = 1f - (1f * max_alpha / 100);
		img = GetComponent<Image>();
	}

	void Update() {
		mina = 1f - (1f * min_alpha / 100);
		maxa = 1f - (1f * max_alpha / 100);
		// 周期の半分以上でmaxからminへ。(濃くしていく)
		// 逆に周期の半分以下ならminからmaxへ(消していく)
		now ++;
		if (now >= period) {
			now = 0;
		}

		if (now <= period / 2) {
			// 薄くする
			float m = 2f * now / period;
			nowa = (1f - m) * mina + m * maxa;
		} else {
			// 濃くする
			float m = (2f * now - period) / period; 
			nowa = (1f - m) * maxa + m * mina;
		}
		// 変更適用
		img.color = new Color(r, g, b, nowa);
	}
}

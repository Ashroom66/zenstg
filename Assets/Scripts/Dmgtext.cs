using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dmgtext : MonoBehaviour {

	// ポップアップ君は最初上に飛び跳ねて、重力に身を任せフェードアウトしやがて消える

	public float pop;	// ジャンプ力ぅ…ですかね
	public float step;	// 横のばらつき
	public float fadespd;	// フェードアウトする速度
	public float r;		// 色
	public float g;
	public float b;
	private float alpha = 1f;	// 不透明度

	private TextMesh damageText;

	// Use this for initialization
	void Start () {
		// テキスト書き換えの準備
		damageText = GetComponent<TextMesh>();

		// 跳ねる
		GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-step, step),pop,0) , ForceMode2D.Impulse);
		
	}
	
	// Update is called once per frame
	void Update () {
		// ここでフェードと消滅処理
		// 少しずつ透明に
		alpha -= fadespd * Time.deltaTime;

		// 色変更
		damageText.color = new Color(r, g, b, alpha);
		
		if (alpha < 0f) {
			Destroy(gameObject);
		}


		
	}
}

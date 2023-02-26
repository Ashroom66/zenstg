using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatOct : MonoBehaviour {

	// LookAt2Dとは違う方法で。

	// 変数
	public Vector3 vect;	// ルックポイント-自分の位置のベクトル
	public static float angle;		// 角度
	public string chara1;	// キャラアニメーション
	public string chara2;	// 数字はテンキーイメージの方向に対応
	public string chara3;
	public string chara4;
	public string chara6;
	public string chara7;
	public string chara8;
	public string chara9; 
	public Vector3 mousepos;	// マウス座標
	public Vector3 mouseWpos;	// マウス座標(ワールド座標)
	public Vector3 pos;			// 自機座標

	public string chara;	// キャラアニメーション呼び出し用

	// 他スクリプトから角度要求合ったときに角度返すやーつ
	public static float getangleOct() {
		return angle;
	}

	void Start () {
		chara = "ikiso";
	}

	void Update () {
		// マウス位置座標取得、Z軸(一応)補正
		mousepos = Input.mousePosition;
		mousepos.z = 10f;

		// マウス位置座標をスクリーン座標からワールド座標へ
		mouseWpos = Camera.main.ScreenToWorldPoint(mousepos);

		// 自機座標取得
		pos = transform.position;

		// 角度計算
		angle = Mathf.Atan((mouseWpos.x - pos.x)/(mouseWpos.y - pos.y)) * 180f / Mathf.PI;

		// 角度修正の１：y座標がマウスのが下だった場合は180足す
		if (pos.y - mouseWpos.y > 0) {
			angle += 180;
		}

		// 角度修正の2：90度足す
		angle += 90;

		// これで角度は左を基準に0～360度になった	やったぜ。
		// 後は角度に合わせてキャラ表示を変えればおｋ

		
		chara = "";
		

		if (angle >= 337.5 || angle <= 22.5 ) {
			// 左
			chara += chara4;
		} else if (angle <= 67.5) {
			// 左上
			chara += chara7;
		} else if(angle <= 112.5) {
			// 上
			chara += chara8;
		} else if(angle <= 157.5) {
			// 右上
			chara += chara9;
		} else if(angle <= 202.5) {
			// 右
			chara += chara6;
		} else if(angle <= 247.5) {
			// 右下
			chara += chara3;
		} else if(angle <= 292.5) {
			// 下
			chara += chara2;
		} else {
			// 左下
			chara += chara1;
		}

		// 静動判定：方向キー押してないならnonを文字列最初に追加
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) {
			chara += "not";
		}
//		GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load (chara));
		GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(chara);

	}
}

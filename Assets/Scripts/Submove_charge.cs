using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submove_charge : MonoBehaviour {
	// 回避動作時に生成されるタックル用のアレ。
	// 貫通性能は無いが一撃与えると判定だけ消す。
	// 消滅はこいつ自身の黒化

	private BulletBase bulletbase;	// ダメージ適用の為に呼び出し
	private Player player;
	private SpriteRenderer bullet;	// 色変更とか。

	public int atk;
	public int delValue;	// 生存時間=効果適用時間
	private int delCount;
	private float colorvalue;
	
	void Start () {
		// ベース君呼び出し
		bulletbase = GetComponent<BulletBase>();
		// バレットベースへ火力適用。チャージは自身の突撃力(回避力)も考慮してみる
		player = GameObject.Find("Player").GetComponent<Player>();
		bulletbase.dmg = (int)(atk * ((player.ps.atk_boost + 100f)/100) * (player.avoidance + 100f) / 100f);

		bullet = GetComponent<SpriteRenderer>();
	}

	void Update () {
		delCount ++;
		colorvalue = 1f - (delCount * 1f / delValue);
		bullet.color = new Color(colorvalue, colorvalue, colorvalue, (2f+colorvalue)/3f - 0.6f);

		if (colorvalue <= 0f) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay2D (Collider2D c) {
		if (c.gameObject.tag == "Enemybody") {
			// コンポーネント削除
			Destroy(this.GetComponent<PolygonCollider2D>());
		}
		
	}

}

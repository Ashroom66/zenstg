using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_speed1 : MonoBehaviour {
	// ベース呼び出し準備
	BulletBase bulletbase;

	// 弾情報
	public float speed;
	public int delMode;
	public float delValue;
	public int level;		// レベル
	public float accurate;	// 精度(speed弾はバラつく)

	
	void Start () {
		// ベース君呼び出し
		bulletbase = GetComponent<BulletBase>();

		// 弾設定引き継ぎ
		bulletbase.speed = speed;
		bulletbase.delMode = delMode;
		bulletbase.delValue = delValue;

		// 弾動かす

		
	}

	void Update() {


		


	}
	
}

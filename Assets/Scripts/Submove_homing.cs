using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submove_homing : MonoBehaviour {
	// ホーミングするつよそうなやつ

	// ベース呼び出し
	BulletBase bulletbase;

	// 弾情報：弾ごとにせてい
	// 変数は火力、基本弾速、delmode、delvalue、サーチ範囲、追尾力を予定。
	public int atk;
	public float speed;	// 基本の弾速
	public float speedlimit;	// 最大弾速(超えたらaddforceで後ろ向きにちょい引っ張る)
	public int delMode;			// 多分0の生存時間固定だろうね…( ˘ω˘ )
	public float delValue;
	public float search;
	public float homingpw;	// 追尾力ぅ…ですかね
	public bool isEnemyBullet;	// 敵の弾か否か
	public bool isHoming2;		// ホーミングの2は消滅時に置き土産を残す
	public GameObject explosion;

	// 追尾に使用する
	private GameObject nearestEnemy;	// 最終的に最寄りの敵を示す
	private float minDis;				// 一番近いキョリ
	private GameObject[] enemys;		// タグヒットした敵共
//	private GameObject enemy;			// 距離判定用
	private float dis; 					// 距離判定用

	// 最高速度制御に使用
	private Vector3 newpos;
	private Vector3 oldpos;
	private float posdist;

	// 火力ブースト考慮用
	private Player player;

	void Start () {
		// ベース呼び出し
		bulletbase = GetComponent<BulletBase>();
		// バレットベースへ火力適用
		player = GameObject.Find("Player").GetComponent<Player>();
		if (gameObject.tag != "EnemyBullet") {

			bulletbase.dmg = (int)(atk * ((player.ps.atk_boost + 100f)/100));
		} else {
			bulletbase.dmg = atk;
		}

		// 初動は直進
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;

		oldpos = transform.position;
		
	}
	
	void Update () {
		// Delete herself
		bulletbase.Delete(delMode, delValue);

		// 向き変更
		newpos = transform.position;
		if (newpos.x - oldpos.x < 0) {
			transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan((newpos.y - oldpos.y)/(newpos.x - oldpos.x)) * 180 / Mathf.PI) + 90  );
		} else if (newpos.x - oldpos.x > 0) {
			transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan((newpos.y - oldpos.y)/(newpos.x - oldpos.x)) * 180 / Mathf.PI) - 90  );
		}
		
		// 最高速度制御
		posdist = Vector3.Distance(newpos, oldpos);
		if (posdist * 60 > speedlimit) {
			this.GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speedlimit;
		} 

		if (isEnemyBullet == false) {
			// 最も近い敵をサーチ
			nearestEnemy = null;
			minDis = search;
			enemys = GameObject.FindGameObjectsWithTag("Enemybody");
			foreach(GameObject enemy in enemys) {
				dis = Vector3.Distance(transform.position, enemy.transform.position);
				if (dis <= minDis) {
					minDis = dis;
					nearestEnemy = enemy;
				}
			}
			if (nearestEnemy != null) {
				// 敵がいるのでその方向へaddforce
				// 近づく程ホーミング性能強くする
				Vector2 force = new Vector2(nearestEnemy.transform.position.x - transform.position.x,nearestEnemy.transform.position.y - transform.position.y).normalized;
				this.GetComponent<Rigidbody2D>().AddForce(force * homingpw / (minDis * 10) , ForceMode2D.Impulse);

			}
		} else {
			// 敵の弾によるプレイヤーへのプレゼント :)
			minDis = search;
			dis = Vector3.Distance(transform.position, player.transform.position);
			if (dis < minDis) {
				Vector2 force = new Vector2(player.transform.position.x - transform.position.x,player.transform.position.y - transform.position.y).normalized;
				this.GetComponent<Rigidbody2D>().AddForce(force * homingpw / (dis * 10) , ForceMode2D.Impulse);
			}
			
		}
		



		
		// pos更新
		oldpos = transform.position;


	}

	void OnTriggerEnter2D (Collider2D c) {
		if ((isEnemyBullet == true && c.gameObject.tag == "Playerbody") || (isEnemyBullet == false && c.gameObject.tag == "Enemybody")) {
			if (isHoming2 == true) {
				bulletbase.Shot(transform.position, transform.rotation, explosion, 0, 0);
			}
			Destroy(this.gameObject);
		}
		
	}

	
}

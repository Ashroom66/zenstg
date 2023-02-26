using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nearest_enemy : MonoBehaviour {
	// ホーミングスクリプトの処理内容を利用して敵のいる方向求めて
	// 良い感じに表示しちゃおう！ってやつ

	// プレイヤー座標基準、円形とかもプレイヤーに合わせて動かしちゃおう :)

	private GameObject nearestEnemy;
	private float minDis;
	private GameObject[] enemys;
	private float dis;
	public float searchlimit;	// 一定距離以下は薄くする為。ってかナビ君の位置もこれに合わせる
	private Transform player;
	private GameObject navi;
	private SpriteRenderer naviimage;
	private SpriteRenderer myimage;
	private float theta;
	private float navix;
	private float naviy;

	void Start () {
		player = GameObject.Find("Player").transform;
		navi = GameObject.Find("navi");
		naviimage = navi.GetComponent<SpriteRenderer>();
		myimage = this.GetComponent<SpriteRenderer>();
	}

	void Update () {
		
		// 最も近い敵をサーチ
		nearestEnemy = null;
		minDis = 1000f;
		enemys = GameObject.FindGameObjectsWithTag("Enemybody");
		foreach(GameObject enemy in enemys) {
			dis = Vector3.Distance(transform.position, enemy.transform.position);
			if (dis <= minDis) {
				minDis = dis;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null) {
		//	theta = Mathf.Atan(((nearestEnemy.transform.position.y - player.position.y) * Mathf.PI)/((nearestEnemy.transform.position.x - player.position.x) * 180f) );
			theta = Mathf.Atan2(nearestEnemy.transform.position.x - player.position.x, nearestEnemy.transform.position.y - player.position.y) * Mathf.Rad2Deg * -1 + 90;
			navi.transform.rotation = Quaternion.Euler(0,0,(theta - 90));
			navix = player.position.x + (searchlimit * Mathf.Cos(theta * Mathf.PI / 180f));
			naviy = player.position.y + (searchlimit * Mathf.Sin(theta * Mathf.PI / 180f));
			navi.transform.position = new Vector3(navix, naviy, 0);
			transform.position = player.position;

			if (minDis < searchlimit) {
				myimage.color = new Color(0,0,0,0.02f);
				naviimage.color = new Color(0,0,0,0.1f);
			} else {
				myimage.color = new Color(0,0,0,0.06f);
				naviimage.color = new Color(0,0,0,0.4f);
			}
		}
	}
}

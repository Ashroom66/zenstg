using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedWorld : MonoBehaviour {
	// border君のサイズが両端の限界座標
	// 限界座標超えたら執行

	private float sizex;
	private float sizey;
	private SpriteRenderer border;
	private GameObject[] crafts;
	private GameObject player;
	public float forcepw;	// 押し出す力
	private Vector3 force;
	private float dist;		// 外に出た距離(遠い程強く)
	private float x;		// 座標補正：自分の中心座標使って対象座標補正、一般化の為
	private float y;


	void Start () {
		border = GetComponent<SpriteRenderer>();
		player = GameObject.Find("Player");
		sizex = border.size.x;
		sizey = border.size.y;
	}

	void Update () {
		crafts = GameObject.FindGameObjectsWithTag("Enemybody");
		foreach(GameObject craft in crafts) {
			OutOfBorder(craft);
		}
		OutOfBorder(player);
	}

	void OutOfBorder (GameObject a) {
		x = a.transform.position.x - transform.position.x;
		y = a.transform.position.y - transform.position.y;
		if (x < -1*sizex) {
			dist = (x + sizex) * -1;
			force = new Vector3(1,0,0);
			a.GetComponent<Rigidbody2D>().AddForce(forcepw * force * dist * dist / 10, ForceMode2D.Impulse);
		} else if (sizex < x) {
			dist = x - sizex;
			force = new Vector3(-1,0,0);
			a.GetComponent<Rigidbody2D>().AddForce(forcepw * force * dist * dist / 10, ForceMode2D.Impulse);
		}
		if (y < -1*sizey) {
			dist = (y + sizey) * -1;
			force = new Vector3(0,1,0);
			a.GetComponent<Rigidbody2D>().AddForce(forcepw * force * dist * dist / 10, ForceMode2D.Impulse);
		} else if (sizey < y) {
			dist = y - sizey;
			force = new Vector3(0,-1,0);
			a.GetComponent<Rigidbody2D>().AddForce(forcepw * force * dist * dist / 10, ForceMode2D.Impulse);
		}
	}
}

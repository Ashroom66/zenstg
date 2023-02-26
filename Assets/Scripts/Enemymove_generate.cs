using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove_generate : MonoBehaviour {
	// count1とchargeを生成
	// 配置決め
	// 机固定箇所は死
	// 外に机出していいか否か
	EnemyBase enemybase;
	public GameObject generate1;
	public GameObject generate2;
	public GameObject generate3;
	public GameObject generate4;
	public GameObject generate5;
	public int num1min;	// 同時生成数
	public int num1max;
	public int num2min;
	public int num2max;
	public int num3min;
	public int num3max;
	public int num4min;
	public int num4max;
	public int num5min;
	public int num5max;
	public int generatectmin;
	public int generatectmax;
	public float genbarapos;
	public float genbararot;
	private int generatect;
	private int genetype;
	private Vector3 genpos;
	private Quaternion genrot;

	void Start () {
		generatect = Random.Range(generatectmin, generatectmax);
		enemybase = GetComponent<EnemyBase>();
	}

	void Update () {
		if (enemybase.active == true) {
			if (generatect > 0) {generatect --;}
			if (generatect <= 0) {
				// 敵生成
				genetype = Random.Range(1,5+1);
				if (genetype == 1) {
					for (int i = 0; i < Random.Range(num1min, num1max+1); i++) {
						genpos = new Vector3(transform.position.x + Random.Range(-1f * genbarapos, genbarapos), transform.position.y + Random.Range(-1f * genbarapos, genbarapos), 0);
						genrot.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-1f * genbararot, genbararot));
						Instantiate(generate1, genpos, genrot);
					}
				} else if (genetype == 2) {
					for (int i = 0; i < Random.Range(num2min, num2max+1); i++) {
						genpos = new Vector3(transform.position.x + Random.Range(-1f * genbarapos, genbarapos), transform.position.y + Random.Range(-1f * genbarapos, genbarapos), 0);
						genrot.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-1f * genbararot, genbararot));
						Instantiate(generate2, genpos, genrot);
					}
				} else if (genetype == 3) {
					for (int i = 0; i < Random.Range(num3min, num3max+1); i++) {
						genpos = new Vector3(transform.position.x + Random.Range(-1f * genbarapos, genbarapos), transform.position.y + Random.Range(-1f * genbarapos, genbarapos), 0);
						genrot.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-1f * genbararot, genbararot));
						Instantiate(generate3, genpos, genrot);
					}
				} else if (genetype == 4) {
					for (int i = 0; i < Random.Range(num4min, num4max+1); i++) {
						genpos = new Vector3(transform.position.x + Random.Range(-1f * genbarapos, genbarapos), transform.position.y + Random.Range(-1f * genbarapos, genbarapos), 0);
						genrot.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-1f * genbararot, genbararot));
						Instantiate(generate4, genpos, genrot);
					}
				} else if (genetype == 5) {
					for (int i = 0; i < Random.Range(num5min, num4max+1); i++) {
						genpos = new Vector3(transform.position.x + Random.Range(-1f * genbarapos, genbarapos), transform.position.y + Random.Range(-1f * genbarapos, genbarapos), 0);
						genrot.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + Random.Range(-1f * genbararot, genbararot));
						Instantiate(generate5, genpos, genrot);
					}
				}
				generatect = Random.Range(generatectmin, generatectmax);
			}
		}
		
	}
	
}

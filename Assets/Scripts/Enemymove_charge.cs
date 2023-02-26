using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove_charge : MonoBehaviour {
	// 非アクティブ時静止
	// アクティブ時プレイヤー側へ方向転換、適当なタイミングで悪質タックル
	// ついでにアクティブ時は適当に回避動作入れるゾ :)

	// ベースとか
	EnemyBase enemybase;
	Vector3 playerpos;

	// 回避動作関係(回避力はベースのspeed使う)
    public int avoctmin;// 回避ct
    public int avoctmax;
    private int avoct;  // 回避ctカウント用
	public int rotatespd;   // 回転速度(1が最大、0が方向固定)
	public bool firstavoctunit;	// 回避CTを合わせる時にチェック

	// 突撃関係
	private bool assault = false;	// 突撃中か否か
	public int chargectmin;	// 突撃ct
	public int chargectmax;
	private int chargect;
	public int chargetime;	// 突撃時間
	private int chargetimenow;
	public float chargepw;
	public bool firstchargectunit;	// 突撃CT併せるときにチェック

	private Vector3 angle;
	private Vector2 force;

	void Start () {
        enemybase = GetComponent<EnemyBase>();
		if (firstavoctunit == true) {
			avoct = avoctmin;
		} else {
			avoct = Random.Range(avoctmin, avoctmax);
		}
        if (firstchargectunit == true) {
			chargect = chargectmin;
		} else {
			chargect = Random.Range(chargectmin, chargectmax);	
		}
		chargect = Random.Range(chargectmin, chargectmax);
	}

	void Update () {
		if (enemybase.active == false) {
			// 非アクティブ時の行動

		} else {
			// アクティブ時の行動
			if (assault == false) {
				// 回転
				playerpos = GameObject.Find("Player").transform.position;
	            float dx =  playerpos.x - transform.position.x;
    	        float dy = playerpos.y - transform.position.y;
        	    float rad = Mathf.Atan2(dx,dy) * Mathf.Rad2Deg * (-1);
        	    float myrot = transform.localEulerAngles.z;
            	while (myrot > 180f) {myrot -= 360;}
            	while (myrot < -180f) {myrot += 360;}
            	if (rad - myrot > 180f) {rad -= 360;}
            	if (rad - myrot < -180f) {myrot -= 360f;}
            	transform.Rotate (0,0,(rad - myrot) / rotatespd);

				// 回避
				if (avoct > 0) {avoct --;}  // 回避ct減少
				if (avoct <= 0) {
            	    Vector2 muki = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
                	this.GetComponent<Rigidbody2D>().AddForce(muki *Random.Range(enemybase.speed/2, enemybase.speed), ForceMode2D.Impulse);

                	avoct = Random.Range(avoctmin, avoctmax);
            	}
			}
			

			// 突撃関係
			if (chargect > 0) {chargect --;}
			if (chargect <= 0) {
				if (assault == false) {
					// 突撃開始
					assault = true;
					chargetimenow = chargetime;
				}
				chargetimenow --;
				// 突撃
				angle = transform.localEulerAngles;
				force = new Vector2(Mathf.Cos((angle.z - 270) * Mathf.PI / 180), Mathf.Sin((angle.z - 270) * Mathf.PI / 180));
				this.GetComponent<Rigidbody2D>().AddForce(force * chargepw);

				if (chargetimenow <= 0) {
					// 突撃終了
					assault = false;
					chargect = Random.Range(chargectmin, chargectmax);
				}

			}

			
		}
	}



}

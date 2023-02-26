using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove_count1 : MonoBehaviour {
    // 非アクティブ時静止
    // アクティブ時主人公の方向を(徐々に)向き、適度に回避しつつ弾を撃つ
    // speedを回避に使用
    // 弾を撃つ：BulletBase使用

    // ベース呼び出し
    EnemyBase enemybase;
    BulletBase bulletbase;
    Vector3 playerpos;

    public int avoctmin;// 回避ct
    public int avoctmax;
    public int mode;    // 弾撃ち方
    public int rotatespd;   // 回転速度(1が最大、0が方向固定)
    private int avoct;  // 回避ctカウント用

    // 弾撃ち関連
    public int bullet_ct1;  // 連射間ct
    public int bullet_ct2;  // 連射後ct
    public int blaze;       // 連射回数
    public int way;         // way数(1で直線)       way関連はSubcon参考
    public float wayint;    // way弾間、角度のずれ
    private int bullet_ct;  // ctカウント用
    private int blazenow;   // 連射回数カウント用

	// 発射地点算出用ベクター君用意
	Vector3 shotorigin;
	Quaternion rot;

    void Start () {
        enemybase = GetComponent<EnemyBase>();
        bulletbase = GetComponent<BulletBase>();
        avoct = Random.Range(avoctmin, avoctmax);
        bullet_ct = Random.Range(0, bullet_ct2);
    }

    void Update() {

        // 動作関連
        if (enemybase.active == false) {
            // 非アクティブ時の行動


        } else {
            // アクティブ時の行動
            if (bullet_ct > 0) { bullet_ct --; }    // 弾ctカウント
            if (avoct > 0) {avoct --;}  // 回避ct減少

            // 回転
            playerpos = GameObject.Find("Player").transform.position;
            float dx =  playerpos.x - transform.position.x;
            float dy = playerpos.y - transform.position.y;
            float rad = Mathf.Atan2(dx,dy) * Mathf.Rad2Deg * (-1);

            // ヒント：境目
            float myrot = transform.localEulerAngles.z;
            
            while (myrot > 180f) {myrot -= 360;}
            while (myrot < -180f) {myrot += 360;}
            
            //Debug.Log(rad + " , " + myrot + " , " + (rad - myrot));
            // 下側の、角度変更点以外はうまく処理できた
            if (rad - myrot > 180f) {rad -= 360;}
            if (rad - myrot < -180f) {myrot -= 360f;}
            transform.Rotate (0,0,(rad - myrot) / rotatespd);


            /*
            if (rad - myrot > 180f) {
                while (rad - myrot < 180f) {
                    rad -= 360f;
                }
                transform.Rotate (0,0,(myrot - rad) / rotatespd);
            } else if (rad - myrot < -180f) {
                while (rad - myrot > -180f) {
                    myrot -= 360f;
                }
                transform.Rotate (0,0,(myrot - rad) / rotatespd);
            } else {
                transform.Rotate(0,0,(rad - myrot) / rotatespd);
            }
             */

            // 攻撃            
            if (bullet_ct <= 0) {
                // 発射原点セット(Subcon流用)
				rot = Quaternion.Euler(0,0,transform.localEulerAngles.z);
				shotorigin = new Vector3 (transform.position.x + (Mathf.Cos((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),transform.position.y + (Mathf.Sin((rot.eulerAngles.z + 90) * Mathf.PI / 180) * bulletbase.shotdist ),0);
                if (mode == 1) {
                    // モード1:直線状にany連射
                    blazenow ++;
                    // 新弾発射処理(way考慮)
                    Quaternion rot = Quaternion.Euler(0,0,transform.localEulerAngles.z);
                    // 奇数偶数分け
                    if (way % 2 == 1) {
                        // 奇数
                        rot.eulerAngles -= new Vector3(0,0, wayint * (way / 2));
                    } else {
                        // 偶数
                        rot.eulerAngles -= new Vector3(0,0, (way - 1)* wayint / 2);
                    }
                    // forループで角度足しつつShot
                    for (int i = 1; i <= way; i++) {
                        bulletbase.Shot(shotorigin, rot, bulletbase.image, 0, bulletbase.shotdist);
                        rot.eulerAngles += new Vector3(0,0,wayint);
                    }
                    
                    // bulletbase.Shot(transform, transform.rotation, bulletbase.image, 0f);    // 旧弾発射処理
                    
                    // ct設定
                    if (blazenow >= blaze) {
                        // any連射終了、ctを長い方(ct2)でセット
                        bullet_ct = bullet_ct2;
                        blazenow = 0;
                    } else {
                        bullet_ct = bullet_ct1;
                    }

                }
            }

            // 回避
            
            if (avoct <= 0) {

                // 方向は適当、回避力はavopwで指定(強弱も付けとく)
                Vector2 muki = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
                this.GetComponent<Rigidbody2D>().AddForce(muki *Random.Range(enemybase.speed/2, enemybase.speed), ForceMode2D.Impulse);

                avoct = Random.Range(avoctmin, avoctmax);
            }
        }

        
    }
    

}
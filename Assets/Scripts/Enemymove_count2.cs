using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove_count2 : MonoBehaviour {

	// 強化偏差射撃型AI：プレイヤーの「移動先」を狙う。
	// 非アクティブ時静止
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
    private float rad;

    // 弾撃ち関連
    public int bullet_ct1;  // 連射間ct
    public int bullet_ct2;  // 連射後ct
    public int blaze;       // 連射回数
    public int way;         // way数(1で直線)       way関連はSubcon参考
    public float wayint;    // way弾間、角度のずれ
	public float accurate;	// 正確性。
    private int bullet_ct;  // ctカウント用
    private int blazenow;   // 連射回数カウント用

	// 発射地点算出用ベクター君用意
	Vector3 shotorigin;
	Quaternion rot;

	// 移動先より角度算出用
	Vector2 relpos;	// プレイヤーとの相対座標
    Player playerstat;
	public float usingblspd;
	public float bulletspd;
	public float playerspd;		// 仮調整にpublic化。調整終了後start関数で定義
	private float vx;
	private float vy;
    private float v;
    private float shotdist;
    private float asin;
    private float atan;
    private float relrot;
    private float t;
    private float acos;
    private float radold;
    private float a;
    private float b;
    private float c;
    private float d;
    private float t1;
    private float t2;
    private Vector2 target;
    private Vector2 prerelpos;
    private Vector2 shotfor;

    public Vector2 LinePrediction2(Vector2 shotPosition, Vector2 targetPosition, Vector2 targetPrePosition, float bulletSpeed) {
        //Unityの物理はm/sなのでm/flameにする
        bulletSpeed = bulletSpeed * Time.fixedDeltaTime;
        Vector2 v2_Mv = targetPosition - targetPrePosition;
        Vector2 v2_Pos = targetPosition - shotPosition;

        float A = Vector2.SqrMagnitude(v2_Mv) - bulletSpeed * bulletSpeed;
        float B = Vector2.Dot(v2_Pos, v2_Mv);
        float C = Vector2.SqrMagnitude(v2_Pos);

        //0割禁止
        if (A == 0 && B == 0)return targetPosition;
        if (A == 0 )return targetPosition + v2_Mv * (-C / B / 2);

        //虚数解はどうせ当たらないので絶対値で無視した
        float D = Mathf.Sqrt(Mathf.Abs(B * B - A * C));
        return targetPosition + v2_Mv * PlusMin((-B - D) / A, (-B + D) / A);
    }
    //プラスの最小値を返す(両方マイナスなら0)
    public float PlusMin(float ap, float bp) {
        if (ap < 0 && bp < 0) return 0;
        if (ap < 0) return bp;
        if (bp < 0) return ap;
        return ap < bp ? ap : bp;
    }

    void Start () {
        enemybase = GetComponent<EnemyBase>();
        bulletbase = GetComponent<BulletBase>();
        avoct = Random.Range(avoctmin, avoctmax);
        bullet_ct = Random.Range(0, bullet_ct2);
        playerstat = GameObject.Find("Player").GetComponent<Player>();
        shotdist = bulletbase.shotdist;
        bulletspd = 0.85f;
        playerspd = 1.02f;
        
    }

    void Update() {

        // 動作関連
        if (enemybase.active == false) {
            // 非アクティブ時の行動


        } else {
            // アクティブ時の行動
            if (bullet_ct > 0) { bullet_ct --; }    // 弾ctカウント
            if (avoct > 0) {avoct --;}  // 回避ct減少
            /*
			古いやつ
            vx = Input.GetAxisRaw("Horizontal");
			vy = Input.GetAxisRaw("Vertical");
			if(Input.GetKey(KeyCode.H)) {vx = -1;}
			if(Input.GetKey(KeyCode.K)) {vx = 1;}
			if(Input.GetKey(KeyCode.U)) {vy = 1;}
			if(Input.GetKey(KeyCode.J)) {vy = -1;}
            vx *= playerspd * playerstat.ps.maxiV / 10;
            vy *= playerspd * playerstat.ps.maxiV / 10;
            if(vx != 0 && vy != 0) {vx *= 1 /Mathf.Sqrt(2); vy *= 1 /Mathf.Sqrt(2); }
            
            */

            // 回転
            playerpos = GameObject.Find("Player").transform.position;
				// 角度変更点
			relpos = new Vector2(playerpos.x - transform.position.x, playerpos.y - transform.position.y);
            // asin =  Mathf.Asin((relpos.x * vy  - relpos.y * vx) / (Mathf.Sqrt((bulletspd * usingblspd * relpos.x - shotdist * vx)*(bulletspd * usingblspd * relpos.x - shotdist * vx) + (shotdist * vy - bulletspd * usingblspd * relpos.y)*(shotdist * vy - bulletspd * usingblspd * relpos.y))));
            /*if(asin >= 0) {
                atan = Mathf.Atan((shotdist * vy - bulletspd * usingblspd * relpos.y) / (bulletspd * usingblspd * relpos.x - shotdist * vx)) ;
                rad = (asin - atan) * Mathf.Rad2Deg + 90;
            } else {
                Mathf.Atan((-1 * shotdist * vy - bulletspd * usingblspd * relpos.y) / (bulletspd * usingblspd * relpos.x - shotdist * vx)) ;
                rad = (asin - atan) * -1 * Mathf.Rad2Deg + 90;
            }*/

            // て   す  と
            vx = relpos.x - prerelpos.x;
            vy = relpos.y - prerelpos.y;
            v = usingblspd * Time.fixedDeltaTime;   // m/s => m/flame
            vx *= playerspd;
            vy *= playerspd;
            v *= bulletspd;
            //Debug.Log("vx:" + vx + " vy:" + vy + " v:" + v);
            
            a = vx * vx + vy * vy - v * v;
            b = relpos.x * vx + relpos.y * vy - shotdist * v;
            c = relpos.x * relpos.x + relpos.y * relpos.y - shotdist * shotdist;
            d = Mathf.Sqrt(b * b - a * c);
            Debug.Log("abcd: " + a + ", " + b + ", " + c + ", " + d );
            t1 = (-1*b + d) / a;
            t2 = (-1*b - d) / a;
            
            /* 
            a = vx * vx + vy * vy - v * v;
            b = 2 * (relpos.x * vx + relpos.y * vy);
            c = relpos.x * relpos.x + relpos.y * relpos.y;
            d = Mathf.Sqrt(b * b - 4 * a * c);
            Debug.Log("abcd: " + a + ", " + b + ", " + c + ", " + d + ", relpos:" + relpos.x + ", " + relpos.y);
            t1 = ((-1*b + d) / (2*a)) - shotdist*v;
            t2 = ((-1*b - d) / (2*a)) - shotdist*v;
            */
            if(t1 < 0 && t2 < 0) {
                t = 0;
            } else if(t1 >= 0 && t2 >= 0) {
                if(t1 < t2) {
                    t = t1;
                } else {
                    t = t2;
                }
            } else {
                Debug.Log("else");
                if(t1 >= 0) {
                    t = t1;
                } else {
                    t = t2;
                }
            }
            

            target = new Vector2(relpos.x + vx * t, relpos.y + vy * t);
            rad = Mathf.Atan(target.y / target.x) * Mathf.Rad2Deg - 90;
            if(target.x < 0) {rad += 180;}
            
            /* 
            shotfor = LinePrediction2(new Vector2(transform.position.x, transform.position.y), new Vector2(playerpos.x, playerpos.y), new Vector2(prerelpos.x + transform.position.x, prerelpos.y + transform.position.y), usingblspd);
            target = shotfor;
            rad = Mathf.Atan(target.y / target.x) * Mathf.Rad2Deg - 90;
            //if(target.x < 0) {rad += 180;}
            Debug.Log(playerpos.x + ", " + playerpos.y + " : " + target.x + ", " + target.y);
            */
            prerelpos = new Vector2(relpos.x, relpos.y);

            /*
            if(relpos.x <= 0) {
                //rad = (Mathf.Asin((relpos.x * vy  - relpos.y * vx) / (bulletspd * usingblspd * Mathf.Sqrt(relpos.x * relpos.x + relpos.y * relpos.y))) - Mathf.Atan(-1 * relpos.y / relpos.x) ) * Mathf.Rad2Deg  + 90;
                rad = (Mathf.Asin((relpos.x * vy  - relpos.y * vx) / (Mathf.Sqrt((bulletspd * usingblspd * relpos.x - shotdist * vx)*(bulletspd * usingblspd * relpos.x - shotdist * vx) + (shotdist * vy - bulletspd * usingblspd * relpos.y)*(shotdist * vy - bulletspd * usingblspd * relpos.y)))) - Mathf.Atan((shotdist * vy - bulletspd * usingblspd * relpos.y) / (bulletspd * usingblspd * relpos.x - shotdist * vx)) ) * Mathf.Rad2Deg  + 90;
            } else {
                vx *= -1;
                vy *= -1;
                //rad = (Mathf.Asin((relpos.x * vy - relpos.y * vx) / (bulletspd * usingblspd * Mathf.Sqrt(relpos.x * relpos.x + relpos.y * relpos.y))) - Mathf.Atan( relpos.y / relpos.x) )  * Mathf.Rad2Deg * -1  + 90;
                rad = (Mathf.Asin((relpos.x * vy  - relpos.y * vx) / (Mathf.Sqrt((bulletspd * usingblspd * relpos.x - shotdist * vx)*(bulletspd * usingblspd * relpos.x - shotdist * vx) + (shotdist * vy - bulletspd * usingblspd * relpos.y)*(shotdist * vy - bulletspd * usingblspd * relpos.y)))) - Mathf.Atan(-1 * (shotdist * vy - bulletspd * usingblspd * relpos.y) / (bulletspd * usingblspd * relpos.x - shotdist * vx)) ) * Mathf.Rad2Deg * -1  + 270;
                atan = Mathf.Atan(-1 * (shotdist * vy - bulletspd * usingblspd * relpos.y) / (bulletspd * usingblspd * relpos.x - shotdist * vx)) ;
                relrot = Mathf.Atan(relpos.y/relpos.x) * Mathf.Rad2Deg + 270;
                
            } */

            //Debug.Log("Asin: " + Mathf.Asin((relpos.x * vy  - relpos.y * vx) / (Mathf.Sqrt((bulletspd * usingblspd * relpos.x - shotdist * vx)*(bulletspd * usingblspd * relpos.x - shotdist * vx) + (shotdist * vy - bulletspd * usingblspd * relpos.y)*(shotdist * vy - bulletspd * usingblspd * relpos.y)))) + "   Atan: " + Mathf.Atan((shotdist * vy - bulletspd * usingblspd * relpos.y) / (bulletspd * usingblspd * relpos.x - shotdist * vx)) + "   rad: " + rad);


            // ヒント：境目
            float myrot = transform.localEulerAngles.z;
            
            while (myrot > 180f) {myrot -= 360;}
            while (myrot < -180f) {myrot += 360;}
            
            //Debug.Log(rad + " , " + myrot + " , " + (rad - myrot));
            // 下側の、角度変更点以外はうまく処理できた
            if (rad - myrot > 180f) {rad -= 360;}
            if (rad - myrot < -180f) {myrot -= 360f;}
            transform.Rotate (0,0,(rad - myrot) / rotatespd);

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
                        bulletbase.Shot(shotorigin, bulletbase.diffusion(rot, accurate), bulletbase.image, 0, bulletbase.shotdist);
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

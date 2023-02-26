using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	// ターゲット用変数
	public GameObject target;

	// cam座標x/y
	Vector2 pos;

	// 計算用
	Vector2 move;

	// 縮小用：距離に応じて縮小
	// camとターゲットとの距離
	float dist;

	// デフォ距離
	public float distdefault;

	// 縮小度合い
	public float zoomout; 
	


	// 割合
	public float speed;

	// マウスホイール調整
	private float wheel;
	private float zoompitch;
	private float wheellim = 3;

	void Start () {
		// ターゲット(自機)を探す
		target = GameObject.Find("Player");

	}

	void Update () {
		// マウスホイール
		wheel = Input.GetAxis("Mouse ScrollWheel");
		zoompitch += wheel * 2;
		if(zoompitch > wheellim) {zoompitch = wheellim;}
		if(zoompitch <  -1 * wheellim) {zoompitch = -1 * wheellim;}

		// 一定の"割合"でターゲット追従
		// z座標は-10固定 
		pos = new Vector2(transform.position.x, transform.position.y);

		move = new Vector2((target.transform.position.x - pos.x), (target.transform.position.y - 0.7f - pos.y));

		// カメラサイズ制御
		dist = Mathf.Sqrt(move.x * move.x + move.y * move.y);

		

		transform.position = new Vector3 (pos.x + (move.x / speed), pos.y + (move.y / speed), - distdefault + zoompitch);

		// カメラサイズ制御：遠くになるほどズームアウト
		GetComponent<Camera>().fieldOfView = 60 + (dist * zoomout);

		 // すくしょ
		if (Input.GetKeyDown(KeyCode.T)) {
			CaptureScreenshot.Capture();
		}

		// かーそる画面外オンオフ
		if (Input.GetKeyDown(KeyCode.M)) {
			if(Cursor.lockState == CursorLockMode.Confined) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Confined;
			}
		}
		
		// ノルマ
		if (Input.GetKeyDown(KeyCode.Escape)) {
        	Application.Quit();
		}
	}
}

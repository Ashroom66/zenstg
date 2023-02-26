using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat2D : MonoBehaviour {

	Plane plane = new Plane();
	float distance = 0;

	void Start()
	{
		// 2Dは高さが変わらないので、パラメータ更新せず使いまわしても問題ないはず
		plane.SetNormalAndPosition (Vector3.back, transform.localPosition);
	}

	void Update () 
	{
		// マウスの位置を元にPlaneへの距離を取得
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if( plane.Raycast (ray, out distance)){
			//Planeとの交点を求めて、キャラクターを向ける
			var lookPoint = ray.GetPoint(distance);;
			transform.LookAt( transform.localPosition + Vector3.forward, lookPoint - transform.localPosition);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour {
	//ダメージを受けたときにDamageUIオブジェ探してこれを呼び出す

	public GameObject text;
//	Camera cam;

	void Start () {
//		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	public void DmgPopUp(Vector3 pos, int value) {
		var o = Instantiate(text);
		o.transform.localPosition = pos;
	//	o.transform.position = cam.WorldToScreenPoint(pos);
		o.transform.parent = transform;
		o.GetComponent<TextMesh>().text = value.ToString();

	}
	
}

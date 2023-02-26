using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class Title : MonoBehaviour {
	StreamReader sr;
	FileInfo fi;
	string highscore;
	Text highscoretext;

	void Start () {
		Cursor.lockState = CursorLockMode.Confined;
		
		highscoretext = GameObject.Find("highscore").GetComponent<Text>();
		fi = new FileInfo(Application.dataPath + "/" + "highscore.info");

		if(!File.Exists(Application.dataPath + "/" + "highscore.info")) {
			Debug.Log("null");
			 File.WriteAllText(Application.dataPath + "/" + "highscore.info", "0");
		}

		using (sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)) {
			highscore = sr.ReadToEnd();
		}
		highscoretext.text = "ハイスコア： "+ highscore;
		Save_Load.Resetinfo();

		
	}
	
}

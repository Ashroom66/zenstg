using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CaptureScreenshot : MonoBehaviour {

	public static void Capture ()
    {
        // 別ファイルになるように時刻で出力
        ScreenCapture.CaptureScreenshot(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".png");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotMaker : MonoBehaviour{
    public void MakeScreenShot()
    {
        Application.CaptureScreenshot("Screenshot.png",4);
    }
}

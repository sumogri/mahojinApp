using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotMaker : MonoBehaviour{
    [SerializeField] private RenderTexture renderTexture;

    public void MakeScreenShot()
    {
        //Application.CaptureScreenshot("Screenshot.png",4);
        StartCoroutine(LoadScreenshot());
    }
    private IEnumerator LoadScreenshot()
    {
        yield return new WaitForEndOfFrame();

        //var texture = new Texture2D(Screen.width, Screen.height);
        //texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //texture.Apply();

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../SavadScreen.png",bytes);
    }
}

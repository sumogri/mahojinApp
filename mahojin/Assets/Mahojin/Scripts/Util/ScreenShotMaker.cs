using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;

public class ScreenShotMaker : MonoBehaviour{
    [SerializeField] private RenderTexture renderTexture;
    private string fileName = "SavedScreen";
    private Texture2D texture;

    void Start()
    {

    }


    public void MakeScreenShot()
    {
        StartCoroutine(LoadScreenshot());
    }

    private IEnumerator LoadScreenshot()
    {
        yield return new WaitForEndOfFrame();

        //renderTextureをTexture2Dにしてpngファイル出力する
        texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../" + fileName + ".png",bytes);

        //コマンドを直接たたいて、直前に保存した画像を印刷
        //印刷先はwindows規定のプリンターになる
        /*
        Process exProcess = new Process();
        exProcess.StartInfo.CreateNoWindow = true;
        exProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        exProcess.StartInfo.FileName = "mspaint.exe";
        exProcess.StartInfo.Arguments = "/p " + Application.dataPath + "/../" + fileName + ".png";
        exProcess.Start();
        exProcess.WaitForExit();
        */
        
        //PrintDocumentオブジェクトの作成
        System.Drawing.Printing.PrintDocument pd =
            new System.Drawing.Printing.PrintDocument();

        //PrintPageイベントハンドラの追加
        pd.PrintPage +=
            new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage);
        //印刷を開始する
        pd.Print();
    }


    private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        //画像を読み込む
        Image img = Image.FromFile(Application.dataPath + "/../" + fileName + ".png");
        //画像を描画する
        e.Graphics.DrawImage(img, e.MarginBounds);
        //次のページがないことを通知する
        e.HasMorePages = false;
        //後始末をする
        img.Dispose();
    }
}

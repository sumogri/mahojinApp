using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using UnityEngine;

/// <summary>
/// RenderTextureを印刷するクラス
/// </summary>
public class RenderTexturePrinter : MonoBehaviour{
    [SerializeField] private RenderTexture renderTexture;
    private byte[] textureBytes;

    public Action StartAct { get; set; }   //印刷処理開始時に走る処理
    public Action EndAct { get; set; } //印刷処理終了時に走る処理
    
    public void Print()
    {
        if (StartAct != null) StartAct();

        StartCoroutine(PrintRenderTexture());
    }

    private IEnumerator PrintRenderTexture()
    {
        
        yield return new WaitForEndOfFrame();

        //renderTextureをTexture2Dにしてpngのbyte列を取得
        var texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        textureBytes = texture.EncodeToPNG();
        
        //PrintDocumentを使って印刷
        PrintDocument pd = new PrintDocument();
        pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
        pd.Print();

        if (EndAct != null) EndAct();
    }

    /// <summary>
    /// Imageを印刷するメソッド
    /// </summary>
    private void pd_PrintPage(object sender,PrintPageEventArgs e)
    {
        Image img = (Image) new ImageConverter().ConvertFrom(textureBytes); //byte[]からImage生成
        e.Graphics.DrawImage(img,e.PageBounds);
        e.HasMorePages = false;
        img.Dispose();
    }
}

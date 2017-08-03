using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// RenderTextureを印刷するクラス
/// </summary>
public class RenderTexturePrinter : MonoBehaviour{
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private UnityEvent startEvent;
    [SerializeField] private UnityEvent endEvent;
    [SerializeField] private Dropdown printerDropdown;
    private byte[] textureBytes;
    private PrintDocument printDocument = new PrintDocument();
    

    void Awake()
    {
        // UI周りの初期化
        printDocument.PrintPage += new PrintPageEventHandler(pd_PrintPage);
        printerDropdown.ClearOptions();
        foreach (var name in PrinterSettings.InstalledPrinters)
        {
            printerDropdown.options.Add(new Dropdown.OptionData(name.ToString()));
        }
        printerDropdown.value = printerDropdown.options.FindIndex((x) => 
                x.text == printDocument.PrinterSettings.PrinterName);
    }

    /// <summary>
    /// Printで印刷するプリンター名を変更する
    /// </summary>
    /// <param name="index">InstalledPrintersのindex</param>
    public void SetPrinterName(int index)
    {
        printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[index];
    } 

    /// <summary>
    /// 現在のフレームのrenderTextureを印刷するメソッド
    /// </summary>
    public void Print()
    {
        if (startEvent != null) startEvent.Invoke();

        StartCoroutine(PrintRenderTexture());
    }

    private IEnumerator PrintRenderTexture()
    {
        
        yield return new WaitForEndOfFrame();

        //renderTextureをTexture2Dに変換
        var texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // TODO 非同期にUnity APIを呼ぶ方法を探す
        yield return null; //下の処理で画面が止まるので、一旦戻す
        textureBytes = texture.EncodeToPNG(); //PNGに変換(重い)

        //PrintDocumentを使って印刷
        printDocument.Print();

        if (endEvent != null) endEvent.Invoke();
    }

    /// <summary>
    /// Imageを印刷するメソッド
    /// </summary>
    private void pd_PrintPage(object sender,PrintPageEventArgs e)
    {
        System.Drawing.Image img = (System.Drawing.Image) new ImageConverter().ConvertFrom(textureBytes); //byte[]からImage生成
        e.Graphics.DrawImage(img,e.PageBounds);
        e.HasMorePages = false;
        img.Dispose();
    }
}

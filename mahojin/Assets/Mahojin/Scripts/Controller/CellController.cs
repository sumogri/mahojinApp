using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 魔方陣のセルのコントローラー
/// </summary>
/// <remarks>
/// クリエイトモードでのコントローラー
/// </remarks>
public class CellController : MonoBehaviour,IColorChangeable {
    private InputField field;
    private ColorBlock defaultColorBlock;

    void Start()
    {
        field = GetComponent<InputField>();
        defaultColorBlock = field.colors;
    }

    public void ResetColor()
    {
        field.colors = defaultColorBlock;
    }

    public void SetNormalColor(Color color)
    {
        var colors = field.colors;
        colors.normalColor = color;
        field.colors = colors;
    }

    private bool ContentCheck(string s)
    {
        int val;
        //InputFieldのContotTypeで足りない部分だけを判定
        return int.TryParse(s,out val) && val > 0;
    }
}

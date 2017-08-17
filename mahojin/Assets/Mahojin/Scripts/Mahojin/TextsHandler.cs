using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InfoのTextが使う処理群
/// </summary>
public class TextsHandler : MonoBehaviour{
    [SerializeField] private Color normalColor;

    /// <summary>
    /// normalColorを設定する
    /// </summary>
    /// <remarks>
    /// 設定には、IColorChangeableを使用する
    /// </remarks>
    /// <param name="obj">セット対象のオブジェクト</param>
    public void NormalColorSet(GameObject obj)
    {
        var colorObj = obj.GetComponent<IColorChangeable>();
        if (colorObj == null) return;
        colorObj.SetNormalColor(normalColor);
    }

    /// <summary>
    /// normalColorを引数から再帰的に設定する
    /// </summary>
    /// <param name="root">IColorChangeableを持つオブジェクトの親オブジェクト</param>
    public void NormalColorsSet(GameObject root)
    {
        var colorObjs = root.GetComponentsInChildren<IColorChangeable>();
        foreach(var colorObj in colorObjs)
        {
            colorObj.SetNormalColor(normalColor);
        }
    }

    /// <summary>
    /// ColorResetを引数から再帰的に設定する
    /// </summary>
    /// <param name="root">IColorChangeableを持つオブジェクトの親オブジェクト</param>
    public void ColorResets(GameObject root)
    {
        var colorObjs = root.GetComponentsInChildren<IColorChangeable>();
        foreach (var colorObj in colorObjs)
        {
            colorObj.ResetColor();
        }
    }
}

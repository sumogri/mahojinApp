using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 色を変更する機能をもつことを示すInterface
/// </summary>
public interface IColorChangeable{
    /// <summary>
    /// セルの通常色を変更する
    /// </summary>
    /// <param name="color"></param>
    void SetNormalColor(Color color);

    /// <summary>
    /// セルの色を既定値に戻す
    /// </summary>
    void ResetColor();
}

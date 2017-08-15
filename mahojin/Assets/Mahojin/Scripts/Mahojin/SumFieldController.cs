using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 定和を表示するInputFieldのコントローラー
/// </summary>
public class SumFieldController : MonoBehaviour {
    private InputField myInputField;
    
    void Start()
    {
        myInputField = gameObject.GetComponent<InputField>();
    }
    
    /// <summary>
    /// InputFieldに合計を表示するメソッド
    /// </summary>
    public void TextUpdate()
    {
        var sums = Mahojin.MS4Math.SumFuncs
                    .Select(x => x.Invoke(Mahojin.MagicSquare4Controller.I.MsCells))
                    .Where(x => x.HasValue)
                    .Distinct().ToArray();

        if (sums.Length == 1)
            myInputField.text = sums[0].ToString();
    }

    /// <summary>
    /// 関数を指定して使うTextUpdate
    /// </summary>
    /// <param name="funcId">使用する定和</param>
    public void TextUpdate(int funcId)
    {
        var sums = Mahojin.MS4Math.SumFuncs[funcId].Invoke(Mahojin.MagicSquare4Controller.I.MsCells);
        myInputField.text = sums.Value.ToString();
    }
}

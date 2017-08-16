using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 定和を表示するInputFieldのコントローラー
/// </summary>
public class SumFieldController : MonoBehaviour {
    [SerializeField,Range (0, 9)] private int useFuncId;
    [SerializeField] private GameObject haveCellsObject;
    private Mahojin.IHaveCells haveCells;
    private InputField myInputField;
    
    void Start()
    {
        myInputField = gameObject.GetComponent<InputField>();
        haveCells = haveCellsObject.GetComponent(typeof(Mahojin.IHaveCells)) as Mahojin.IHaveCells;
        TextUpdate();
    }
    
    /// <summary>
    /// InputFieldに合計を表示するメソッド
    /// </summary>
    public void TextUpdate()
    {
        int?[] cells = haveCells.GetCells().Select(x => x.HasValue ? x : 0).ToArray();
        var sums = Mahojin.MS4Math.SumFuncs[useFuncId](cells);
        myInputField.text = sums.ToString();
    }
}

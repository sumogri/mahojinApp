using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 4次方陣を作成するクラス
/// </summary>
public class MagicSquareMaker : SingletonMonoBehaviour<MagicSquareMaker> {
    [SerializeField] private GameObject magicSquare;
    private InputField[] msFields;  //魔方陣のセル

	// Use this for initialization
	void Start () {
        msFields = magicSquare.GetComponentsInChildren<InputField>();
        int?[] tmp = Enumerable.Range(1, 16)
            .Select<int,int?>(x => (x%2==0)? (int?)1 : null )
            .ToArray();
        CellFill(tmp, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 自明なセルを埋めるメソッド
    /// </summary>
    /// <param name="cells">前提とするセルの数値</param>
    /// <param name="sum">魔方陣の定和</param>
    /// <returns></returns>
    private int?[] CellFill(int?[] cells, int sum)
    {
        int[] nullIndex = cells
            .Select((x, i) => new { Content = x, index = i })
            .Where(x => x.Content == null)
            .Select(x => x.index).ToArray();

        foreach (int index in nullIndex)
            Debug.Log(index);

        return cells;
    }
}

using System;
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEditEnd()
    {
        int?[] cells = Enumerable.Repeat<int?>(1, 16).ToArray();
        for (int i = 0; i < 16; i++)
        {
            int a;
            cells[i] = int.TryParse(msFields[i].text, out a) ? (int?)a : null;
        }

        cells = CellFill(cells, 34);
        for (int i = 0; i < 16; i++)
        {
            msFields[i].text = cells[i].ToString();
        }
    }

    /// <summary>
    /// 自明なセルを埋めるメソッド
    /// </summary>
    /// <param name="cells">前提とするセルの数値</param>
    /// <param name="sum">魔方陣の定和</param>
    /// <returns></returns>
    private int?[] CellFill(int?[] cells, int sum)
    {
        cells[0] = sum - (cells[1] + cells[2] + cells[3]) ?? cells[0];
        cells[0] = sum - (cells[1 + 4] + cells[2 + 4 * 2] + cells[3 + 4 * 3]) ?? cells[0];
        cells[0] = sum - (cells[4] + cells[4 * 2] + cells[4 * 3]) ?? cells[0];
        cells[0] = sum - (cells[3] + cells[4 * 3] + cells[3 + 4 * 3]) ?? cells[0];

        return cells;
    }
    
}

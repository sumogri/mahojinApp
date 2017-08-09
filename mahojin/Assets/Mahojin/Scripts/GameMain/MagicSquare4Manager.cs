using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 4次方陣を管理するクラス
/// </summary>
public class MagicSquare4Manager: SingletonMonoBehaviour<MagicSquare4Manager> {
    [SerializeField] private GameObject magicSquare; //魔方陣の親オブジェクト
    private InputField[] msFields;  //魔方陣のセル
    private int sum;     //定和
    private int?[] msCells; //InputFieldを数値化したもの

    public int?[] MsCells { get { return msCells; } }

    // Use this for initialization
    void Start () {
        msFields = magicSquare.GetComponentsInChildren<InputField>();
        sum = 34;
	}
	
	// Update is called once per frame
	void Update () {
        msCells = Enumerable.Repeat<int?>(null, 16).ToArray();
        for (int i = 0; i < 16; i++)
        {
            int a;
            msCells[i] = int.TryParse(msFields[i].text, out a) ? (int?)a : null;
        }
    }

    /// <summary>
    /// 定和を設定するメソッド
    /// </summary>
    /// <param name="str"></param>
    public void SetSum(string str)
    {
        int.TryParse(str, out sum);
    }

    /// <summary>
    /// 今のセルから残りのセルを推測し埋める
    /// </summary>
    public void Assume()
    {
        msCells = MS4Maker.FillBySums(msCells, sum);
        for (int i = 0; i < 16; i++)
        {
            msFields[i].text = msCells[i].ToString();
        }
    }

}

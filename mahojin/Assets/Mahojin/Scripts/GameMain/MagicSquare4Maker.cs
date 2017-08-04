using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 4次方陣を作成するクラス
/// </summary>
public class MagicSquare4Maker : SingletonMonoBehaviour<MagicSquare4Maker> {
    [SerializeField] private GameObject magicSquare;
    private InputField[] msFields;  //魔方陣のセル
    private static  Func<int?[],int,int?>[][] fillFuncs = new Func<int?[], int, int?>[16][];
    private int sum;
    private int?[] msCells;

    public int?[] MsCells { get { return msCells; } }

    // Use this for initialization
    void Start () {
        msFields = magicSquare.GetComponentsInChildren<InputField>();
        sum = 34;
        fillFuncsInit();
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

    public void SetSum(string str)
    {
        int.TryParse(str, out sum);
    }

    public void Assume()
    {
        msCells = CellFill(msCells, sum);
        for (int i = 0; i < 16; i++)
        {
            msFields[i].text = msCells[i].ToString();
        }
    }

    private void fillFuncsInit()
    {
        if (fillFuncs[0] != null) return;

        //セルごとに使う等式を設定
        fillFuncs[0] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1] + c[2] + c[3]),
            (c,s) => s - (c[1+4] + c[2+4*2] + c[3+4*3]),
            (c,s) => s - (c[4] + c[4*2] + c[4*3]),
            (c,s) => s - (c[3] + c[4*3] + c[3+4*3])
        };
        fillFuncs[1] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0] + c[2] + c[3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[1+4*3]),
            (c,s) => s - (c[2] + c[1+4*3] + c[2+4*3]),
        };
        fillFuncs[2] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0] + c[1] + c[3]),
            (c,s) => s - (c[2+4] + c[2+4*2] + c[2+4*3]),
            (c,s) => s - (c[1] + c[1+4*3] + c[2+4*3]),
        };
        fillFuncs[3] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1] + c[2] + c[0]),
            (c,s) => s - (c[2+4] + c[1+4*2] + c[4*3]),
            (c,s) => s - (c[3+4] + c[3+4*2] + c[3+4*3]),
            (c,s) => s - (c[0] + c[4*3] + c[3+4*3])
        };
        fillFuncs[4] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[1+4] + c[2+4] +c[3+4]),
            (c,s) => s - (c[0] + c[4*2] + c[4*3]),
            (c,s) => s - (c[4*2] + c[3+4] + c[3+4*2])
        };
        fillFuncs[5] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4] + c[2+4] + c[3+4]),
            (c,s) => s - (c[0] + c[2+4*2] + c[3+4*3]),
            (c,s) => s - (c[1] + c[1+4*2] + c[1+4*3]),
            (c,s) => s - (c[2+4] + c[1+4*2] + c[2+4*2]),
        };
        fillFuncs[6] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4] + c[1+4] + c[3+4]),
            (c,s) => s - (c[3] + c[1+4*2] + c[4*3]),
            (c,s) => s - (c[2] + c[2+4*2] + c[2+4*3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[2+4*2]),
        };
        fillFuncs[7] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4] + c[1+4] + c[2+4]),
            (c,s) => s - (c[3] + c[3+4*2] + c[3+4*3]),
            (c,s) => s - (c[4] + c[4*2] + c[3+4*2]),
        };
        fillFuncs[8] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[1+4*2] + c[2+4*2] +c[3+4*2]),
            (c,s) => s - (c[0] + c[4*1] + c[4*3]),
            (c,s) => s - (c[4] + c[3+4] + c[3+4*2])
        };
        fillFuncs[9] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4*2] + c[2+4*2] + c[3+4*2]),
            (c,s) => s - (c[3] + c[2+4] + c[4*3]),
            (c,s) => s - (c[1] + c[1+4] + c[1+4*3]),
            (c,s) => s - (c[2+4] + c[1+4] + c[2+4*2]),
        };
        fillFuncs[10] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4*2] + c[1+4*2] + c[3+4*2]),
            (c,s) => s - (c[0] + c[1+4] + c[3+4*3]),
            (c,s) => s - (c[2] + c[2+4] + c[2+4*3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[2+4]),
        };
        fillFuncs[11] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4*2] + c[1+4*2] + c[2+4*2]),
            (c,s) => s - (c[3] + c[3+4] + c[3+4*3]),
            (c,s) => s - (c[4] + c[4*2] + c[3+4]),
        };
        fillFuncs[12] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1+4*3] + c[2+4*3] + c[3+4*3]),
            (c,s) => s - (c[3] + c[2+4] + c[1+4*2]),
            (c,s) => s - (c[4] + c[4*2] + c[0]),
            (c,s) => s - (c[3] + c[0] + c[3+4*3])
        };
        fillFuncs[13] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0+4*3] + c[2+4*3] + c[3+4*3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[1]),
            (c,s) => s - (c[2] + c[1] + c[2+4*3]),
        };
        fillFuncs[14] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0+4*3] + c[1+4*3] + c[3+4*3]),
            (c,s) => s - (c[2+4] + c[2+4*2] + c[2]),
            (c,s) => s - (c[1] + c[1+4*3] + c[2]),
        };
        fillFuncs[15] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1+4*3] + c[2+4*3] + c[0+4*3]),
            (c,s) => s - (c[0] + c[1+4] + c[2+4*2]),
            (c,s) => s - (c[3+4] + c[3+4*2] + c[3]),
            (c,s) => s - (c[0] + c[4*3] + c[3])
        };
    }

    /// <summary>
    /// 自明なセルを埋めるメソッド
    /// </summary>
    /// <param name="cells">前提とするセルの数値</param>
    /// <param name="sum">魔方陣の定和</param>
    /// <returns></returns>
    private int?[] CellFill(int?[] cells, int sum)
    {
        for(int i = 0; i < 16; i++)
        {
            var colors = msFields[i].colors;

            if (cells[i] != null) continue;

            var assumes = fillFuncs[i].Select( (x) => { return x(cells, sum); })
                .Where(x => x.HasValue == true).Distinct().ToArray();
            
            //解が求まる中で唯一になる場合
            if (assumes.Count() == 1)
            {
                cells[i] = assumes[0];
            }
            //求まらない or 矛盾する場合
            else
            {
                colors.normalColor = Color.red;
                msFields[i].colors = colors;
            }
        }

        return cells;
    }
    
}

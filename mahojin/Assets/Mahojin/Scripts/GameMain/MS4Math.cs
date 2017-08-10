using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 4次方陣にかかわる関数群
/// </summary>
public static class MS4Math{
    //あるセルに入るべき数を、定和からの減算で求める式群
    private static Func<int?[], int, int?>[][] cellFuncs = new Func<int?[], int, int?>[16][];

    static MS4Math()
    {
        InitCellFuncs();
    }

    /// <summary>
    /// 自明なセルを埋めるメソッド
    /// 魔方陣の行列対角和を用いて求める
    /// </summary>
    /// <param name="cells">前提とするセルの数値</param>
    /// <param name="sum">魔方陣の定和</param>
    /// <returns></returns>
    public static int?[] FillBySums(int?[] cells, int sum)
    {
        for (int i = 0; i < 16; i++)
        {
            
            if (cells[i] != null) continue;

            var assumes = cellFuncs[i].Select((x) => { return x(cells, sum); })
                .Where(x => x.HasValue == true).Distinct().ToArray();

            //解が求まる中で唯一になる場合
            if (assumes.Count() == 1)
            {
                cells[i] = assumes[0];
            }
        }

        return cells;
    }

    /// <summary>
    /// 各セルに対して、それを求める式の初期化
    /// e.g.)   一行目の各セルの数をa-dとする
    ///         a = sum - b - c - d
    /// </summary>
    private static void InitCellFuncs()
    {
        if (cellFuncs[0] != null) return;

        //セルごとに使う等式を設定
        cellFuncs[0] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1] + c[2] + c[3]),
            (c,s) => s - (c[1+4] + c[2+4*2] + c[3+4*3]),
            (c,s) => s - (c[4] + c[4*2] + c[4*3]),
            (c,s) => s - (c[3] + c[4*3] + c[3+4*3])
        };
        cellFuncs[1] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0] + c[2] + c[3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[1+4*3]),
            (c,s) => s - (c[2] + c[1+4*3] + c[2+4*3]),
        };
        cellFuncs[2] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0] + c[1] + c[3]),
            (c,s) => s - (c[2+4] + c[2+4*2] + c[2+4*3]),
            (c,s) => s - (c[1] + c[1+4*3] + c[2+4*3]),
        };
        cellFuncs[3] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1] + c[2] + c[0]),
            (c,s) => s - (c[2+4] + c[1+4*2] + c[4*3]),
            (c,s) => s - (c[3+4] + c[3+4*2] + c[3+4*3]),
            (c,s) => s - (c[0] + c[4*3] + c[3+4*3])
        };
        cellFuncs[4] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[1+4] + c[2+4] +c[3+4]),
            (c,s) => s - (c[0] + c[4*2] + c[4*3]),
            (c,s) => s - (c[4*2] + c[3+4] + c[3+4*2])
        };
        cellFuncs[5] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4] + c[2+4] + c[3+4]),
            (c,s) => s - (c[0] + c[2+4*2] + c[3+4*3]),
            (c,s) => s - (c[1] + c[1+4*2] + c[1+4*3]),
            (c,s) => s - (c[2+4] + c[1+4*2] + c[2+4*2]),
        };
        cellFuncs[6] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4] + c[1+4] + c[3+4]),
            (c,s) => s - (c[3] + c[1+4*2] + c[4*3]),
            (c,s) => s - (c[2] + c[2+4*2] + c[2+4*3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[2+4*2]),
        };
        cellFuncs[7] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4] + c[1+4] + c[2+4]),
            (c,s) => s - (c[3] + c[3+4*2] + c[3+4*3]),
            (c,s) => s - (c[4] + c[4*2] + c[3+4*2]),
        };
        cellFuncs[8] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[1+4*2] + c[2+4*2] +c[3+4*2]),
            (c,s) => s - (c[0] + c[4*1] + c[4*3]),
            (c,s) => s - (c[4] + c[3+4] + c[3+4*2])
        };
        cellFuncs[9] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4*2] + c[2+4*2] + c[3+4*2]),
            (c,s) => s - (c[3] + c[2+4] + c[4*3]),
            (c,s) => s - (c[1] + c[1+4] + c[1+4*3]),
            (c,s) => s - (c[2+4] + c[1+4] + c[2+4*2]),
        };
        cellFuncs[10] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4*2] + c[1+4*2] + c[3+4*2]),
            (c,s) => s - (c[0] + c[1+4] + c[3+4*3]),
            (c,s) => s - (c[2] + c[2+4] + c[2+4*3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[2+4]),
        };
        cellFuncs[11] = new Func<int?[], int, int?>[]
        {
            (c,s) => s - (c[4*2] + c[1+4*2] + c[2+4*2]),
            (c,s) => s - (c[3] + c[3+4] + c[3+4*3]),
            (c,s) => s - (c[4] + c[4*2] + c[3+4]),
        };
        cellFuncs[12] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1+4*3] + c[2+4*3] + c[3+4*3]),
            (c,s) => s - (c[3] + c[2+4] + c[1+4*2]),
            (c,s) => s - (c[4] + c[4*2] + c[0]),
            (c,s) => s - (c[3] + c[0] + c[3+4*3])
        };
        cellFuncs[13] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0+4*3] + c[2+4*3] + c[3+4*3]),
            (c,s) => s - (c[1+4] + c[1+4*2] + c[1]),
            (c,s) => s - (c[2] + c[1] + c[2+4*3]),
        };
        cellFuncs[14] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[0+4*3] + c[1+4*3] + c[3+4*3]),
            (c,s) => s - (c[2+4] + c[2+4*2] + c[2]),
            (c,s) => s - (c[1] + c[1+4*3] + c[2]),
        };
        cellFuncs[15] = new Func<int?[], int, int?>[] {
            (c,s) => s - (c[1+4*3] + c[2+4*3] + c[0+4*3]),
            (c,s) => s - (c[0] + c[1+4] + c[2+4*2]),
            (c,s) => s - (c[3+4] + c[3+4*2] + c[3]),
            (c,s) => s - (c[0] + c[4*3] + c[3])
        };
    }

    /// <summary>
    /// 自明なセルを埋めるメソッド
    /// 万能方陣を使っている
    /// </summary>
    /// <param name="cells">セルにある数</param>
    /// <param name="sum">定和</param>
    /// <returns>セルに入れるべき数</returns>
    public static int?[] FillByOmnipotent(int?[] cells, int sum)
    {
        // [万能方陣]
        //―――――――――――――――――
        //| a + h | b + g | c + e | d + f |
        //―――――――――――――――――
        //| d + e | c + f | b + h | a + g |
        //―――――――――――――――――
        //| b + f | a + e | d + g | c + h |
        //―――――――――――――――――
        //| c + g | d + h | a + f | b + e |
        //―――――――――――――――――
        // これに従って、行列対角隅端どれか4つが埋まったら、その他のセルを埋める。

        //もし、
        for(int i = 0; i < 4; i++)
        {

        }


        return cells;
    }
}

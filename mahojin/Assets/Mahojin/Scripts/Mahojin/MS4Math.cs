using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mahojin
{
    /// <summary>
    /// 4次方陣にかかわる関数群
    /// </summary>
    public static class MS4Math
    {
        //あるセルに入るべき数を、定和からの減算で求める式群
        private static Func<int?[], int, int?>[][] cellFuncs = new Func<int?[], int, int?>[16][];
        private static List<Func<int?[], int?>> sumFuncs;
        private static List<int[]> sumFulfillIndex;

        /// <summary>
        /// 魔方陣の定和を求める式群
        /// </summary>
        public static List<Func<int?[], int?>> SumFuncs
        {
            get { return sumFuncs; }
        }

        /// <summary>
        /// 定和を満たすIndexのまとまり
        /// </summary>
        public static List<int[]> SumFulfillIndex
        {
            get { return sumFulfillIndex; }
        }

        static MS4Math()
        {
            InitCellFuncs();
            InitSumFuncs();
            InitSumFulfillIndex();
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
        /// 自明なセルを埋めるメソッド
        /// 万能方陣を使っている
        /// </summary>
        /// <param name="cells">セルにある数,すべてのセルについて0以上であること</param>
        /// <param name="sum">定和</param>
        /// <returns>セルに入れるべき数。失敗したとき、cellsを返す</returns>
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
            // これに従って、行列対角隅端どれか1つが埋まったら、その他のセルを埋める。

            int[][][] latinSquares = 
                {new int[][]{new int[]{ 0,1,2,3 },
                             new int[]{ 3,2,1,0 },
                             new int[]{ 1,0,3,2 },
                             new int[]{ 2,3,0,1 } },
                 new int[][]{new int[]{ 7,6,4,5 },
                             new int[]{ 4,5,7,6 },
                             new int[]{ 5,4,6,7 },
                             new int[]{ 6,7,5,4 } } };
            var parms = new int[8];
            var random = new System.Random();

            //定和を満たすセル群
            var sumFulfillCells = sumFulfillIndex.Select(x => x.Select(y => new { Num = cells[y], Index = y }))
                            .Where(x => x.Count(y => y.Num.HasValue) == 4)
                            .FirstOrDefault();

            if (sumFulfillCells == null)  return cells; //一つも埋まってない

            int?[] retCells = new int?[4 * 4];

            //一定回数試行する
            for(int cnt = 0; cnt < 1000; cnt++)
            {
                //万能方陣のパラメータを設定
                foreach (var cell in sumFulfillCells)
                {
                    int[] indexes = {latinSquares[0][cell.Index / 4][cell.Index % 4],
                                 latinSquares[1][cell.Index / 4][cell.Index % 4]};
                    parms[indexes[0]] = random.Next(0, cell.Num.Value);
                    parms[indexes[1]] = cell.Num.Value - parms[indexes[0]];
                }

                for (int i = 0; i < 4 * 4; i++)
                {
                    retCells[i] = parms[latinSquares[0][i / 4][i % 4]] + parms[latinSquares[1][i / 4][i % 4]];
                }

                //独立なセル群になったら、それを返す
                if (retCells.Distinct().Count() == 4 * 4) return retCells;  
            }

            //試行に失敗したので、元のセルを返す
            return cells;
        }

        /// <summary>
        /// セルを定和を満たす単位で切り取るメソッド
        /// </summary>
        /// <remarks>
        /// SumFuncsのように専用メソッドが用意されている場合、そちらを優先して使用すること
        /// </remarks>
        /// <param name="cells">セルの数値</param>
        /// <returns>カットしたセル群</returns>
        public static List<int?[]> Cutout(int?[] cells)
        {
            return sumFulfillIndex.Select(x => x.Select(y => cells[y]).ToArray()).ToList();
        }

        /// <summary>
        /// 魔方陣になっているかを判定するメソッド
        /// ラテン方陣に対応するため、定和だけを見る
        /// </summary>
        /// <param name="cells">セルに入っている数値</param>
        /// <returns>魔方陣であるか</returns>
        public static bool IsMagicSquare(int?[] cells)
        {
            var sums = sumFuncs.Select(x => x.Invoke(cells)).Distinct();

            if (sums.Count() != 1 || !sums.First().HasValue) return false;
            return true;
        }
        
        /// <summary>
        /// 汎魔方陣になっているか判定するメソッド
        /// 数の重複はチェックしない
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static bool IsPanMagicSquare(int?[] cells)
        {
            int?[] pCells = cells.Skip(4).Take(4 * 3).Concat(cells.Take(4)).ToArray();
            bool ret = true;
            
            for(int i = 0; i < 4; i++)
            {
                ret &= IsMagicSquare(pCells);
                pCells = pCells.Skip(4).Take(4 * 3).Concat(pCells.Take(4)).ToArray();
            }

            return ret;
        }

        /// <summary>
        /// ラテン方陣か判定するメソッド
        /// 魔方陣とは違い、対角を見ない
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static bool IsLatinSquare(int?[] cells)
        {
            var sums = sumFuncs.Take(8).Select(x => x.Invoke(cells)).Distinct();

            if (sums.Count() != 1 || !sums.First().HasValue) return false;
            return true;
        }

        /// <summary>
        /// セルの定和を満たす単位ごとのIndexを初期化する
        /// </summary>
        private static void InitSumFulfillIndex()
        {
            sumFulfillIndex = new List<int[]>();

            //列
            foreach (var i in Enumerable.Range(0, 4))
                SumFulfillIndex.Add(new int[] { 0 + 4 * i, 1 + 4 * i, 2 + 4 * i, 3 + 4 * i });
            //行
            foreach (var i in Enumerable.Range(0, 4))
                SumFulfillIndex.Add(new int[] { i, i + 4, i + 4 * 2, i + 4 * 3 });
            //対角
            SumFulfillIndex.Add(new int[] { 0, 1 + 4, 2 + 4 * 2, 3 + 4 * 3 });
            SumFulfillIndex.Add(new int[] { 3, 2 + 4, 1 + 4 * 2, 0 + 4 * 3 });
            //隅と端
            SumFulfillIndex.Add(new int[] { 0, 3, 4 * 3, 3 + 4 * 3 });
            SumFulfillIndex.Add(new int[] { 1 + 4, 2 + 4, 1 + 4 * 2, 2 + 4 * 2 });
            SumFulfillIndex.Add(new int[] { 1, 2, 1 + 4 * 3, 2 + 4 * 3 });
            SumFulfillIndex.Add(new int[] { 4, 4 * 2, 3 + 4, 3 + 4 * 2 });
        }

        /// <summary>
        /// 各セルに対して、それを求める式の初期化
        /// e.g.)   一行目の各セルの数をa-dとする
        ///         a = sum - b - c - d
        /// </summary>
        private static void InitCellFuncs()
        {
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
        /// 定和を計算する式群の初期化
        /// </summary>
        private static void InitSumFuncs()
        {
            sumFuncs = new List<Func<int?[], int?>>();

            //列
            foreach (var i in Enumerable.Range(0, 4).ToArray())
                sumFuncs.Add(m => m[0 + 4 * i] + m[1 + 4 * i] + m[2 + 4 * i] + m[3 + 4 * i]);
            //行
            foreach (var i in Enumerable.Range(0, 4).ToArray())
                sumFuncs.Add(m => m[i] + m[i + 4] + m[i + 4 * 2] + m[i + 4 * 3]);
            //対角
            sumFuncs.Add(m => m[0] + m[1 + 4] + m[2 + 4 * 2] + m[3 + 4 * 3]);
            sumFuncs.Add(m => m[3] + m[2 + 4] + m[1 + 4 * 2] + m[0 + 4 * 3]);
            //隅と端
            sumFuncs.Add(m => m[0] + m[3] + m[4 * 3] + m[3 + 4 * 3]);
            sumFuncs.Add(m => m[1 + 4] + m[2 + 4] + m[1 + 4 * 2] + m[2 + 4 * 2]);
            sumFuncs.Add(m => m[1] + m[2] + m[1 + 4 * 3] + m[2 + 4 * 3]);
            sumFuncs.Add(m => m[4] + m[4 * 2] + m[3 + 4] + m[3 + 4 * 2]);
        }
    }
}
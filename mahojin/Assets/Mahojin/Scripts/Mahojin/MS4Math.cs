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
            // これに従って、行列対角隅端どれか1つが埋まったら、その他のセルを埋める。

            int[][] omnipotent1 = {new int[]{ 0,1,2,3 },
                                  new int[]{ 3,2,1,0 },
                                  new int[]{ 1,0,3,2 },
                                  new int[]{ 2,3,0,1 } };
            int[][] omnipotent2 = {new int[]{ 7,6,4,5 },
                                  new int[]{ 4,5,7,6 },
                                  new int[]{ 5,4,6,7 },
                                  new int[]{ 6,7,5,4 } };

            int?[] nums = Cutout(cells)
                            .Where(x => x.Count(y => y.HasValue) == 4)
                            .FirstOrDefault();

            if (nums == null) return cells; //一つも埋まってない

            int[] parm = new int[8];    //万能方陣の変数

            foreach (var num in nums) { Debug.Log(num); }
            //var random = new System.Random();

            //for(int i = 0; i < 4; i++)
            //{
            //    parm[i] = random.Next(1,nums[i].Value);
            //    parm[i + 4] = nums[i].Value - parm[i];
            //}

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
            foreach (var i in Enumerable.Range(0, 4))
                sumFuncs.Add(m => m[0 + 4 * i] + m[1 + 4 * i] + m[2 + 4 * i] + m[3 + 4 * i]);
            //行
            foreach (var i in Enumerable.Range(0, 4))
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
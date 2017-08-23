using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mahojin
{
    /// <summary>
    /// 3次魔方陣についてのメソッド群
    /// </summary>
    public static class MS3Math
    {
        private static List<Func<int?[], int?>> sumFuncs;

        static MS3Math()
        {
            sumFuncsInit();
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

        private static void sumFuncsInit() {
            sumFuncs = new List<Func<int?[], int?>>();

            //列
            foreach (var i in Enumerable.Range(0, 3))
                sumFuncs.Add(m => m[0 + 3 * i] + m[1 + 3 * i] + m[2 + 3 * i]);
            //行
            foreach (var i in Enumerable.Range(0, 3))
                sumFuncs.Add(m => m[i] + m[i + 3] + m[i + 3 * 2]);

            //対角
            sumFuncs.Add(m => m[0] + m[1 + 3] + m[2 + 3 * 2]);
            sumFuncs.Add(m => m[2] + m[1 + 3] + m[0 + 3 * 2]);
        }
    }
}

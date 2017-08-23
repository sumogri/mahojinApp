using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Mahojin
{
    /// <summary>
    /// 4次方陣を管理するクラス
    /// </summary>
    public class MagicSquare4Controller : MonoBehaviour,IHaveCells
    {
        [SerializeField] private GameObject magicSquare; //魔方陣の親オブジェクト
        [SerializeField] private int sum;     //定和
        [SerializeField] private UnityEvent onValueChanged;
        [SerializeField] private UnityEvent onEndEdit;
        private InputField[] msFields;  //魔方陣のセル
        private int?[] msCells; //InputFieldを数値化したもの

        /// <summary>
        /// 現在のフレームでセルに設定されている数値
        /// </summary>
        public int?[] MsCells { get { return msCells; } }

        void Awake()
        {
            msFields = magicSquare.GetComponentsInChildren<InputField>();
            UpdateMSCells();
        }

        void Update()
        {
            UpdateMSCells();
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
            msCells = MS4Math.FillByOmnipotent(msCells, sum);
            for (int i = 0; i < 16; i++)
            {
                msFields[i].text = msCells[i].ToString();
            }
        }

        /// <summary>
        /// すべてのセルを空にする
        /// </summary>
        public void DeleteAll()
        {
            foreach(var field in msFields)
            {
                field.text = null;
            }
            UpdateMSCells();
            foreach (var field in msFields)
            {
                field.onValueChanged.Invoke(null);
                field.onEndEdit.Invoke(null);
            }
        }

        /// <summary>
        /// 魔方陣に入力が完了した時に走る関数
        /// </summary>
        public void OnEndEdit(){ onEndEdit.Invoke(); }

        /// <summary>
        /// 魔方陣に入力された数が変更されたときに走る関数
        /// </summary>
        public void OnValueChanged(){ onValueChanged.Invoke(); }

        public int?[] GetCells(){ return MsCells; }

        private void UpdateMSCells()
        {
            msCells = Enumerable.Repeat<int?>(null, 16).ToArray();
            for (int i = 0; i < 16; i++)
            {
                int a;
                msCells[i] = int.TryParse(msFields[i].text, out a) ? (int?)a : null;
            }
        }
    }
}
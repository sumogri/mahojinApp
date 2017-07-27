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

    private int[] Assume(int[] cells)
    {
        int sum = cells[0] + cells[1] + cells[2];
        return cells;
    }
}

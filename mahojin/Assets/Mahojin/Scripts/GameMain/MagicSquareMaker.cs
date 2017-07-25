using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicSquareMaker : SingletonMonoBehaviour<MagicSquareMaker> {
    [SerializeField] private GameObject magicSquare;
    private InputField[] msFields;  //魔方陣のセル

	// Use this for initialization
	void Start () {
        msFields = magicSquare.GetComponentsInChildren<InputField>();
        for(int i = 0; i < msFields.Length; i++)
        {
            msFields[i].text = "" + i;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int[] Assume(int[] cells)
    {
        cells[2] = 1;
        Debug.Log(cells);

        return cells;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 総和を表示するコントローラー群を管理するクラス
/// </summary>
/// <remarks>
/// このクラスがアタッチされたオブジェクトが子要素として持つSumFieldControllerを管理する
/// </remarks>
public class SumFieldsManager : MonoBehaviour {
    private SumFieldController[] controllers;

	// Use this for initialization
	void Start () {
        controllers = GetComponentsInChildren<SumFieldController>();
	}
	
    /// <summary>
    /// 管理対象のTextUpdateを呼び出す
    /// </summary>
    public void TextUpdates()
    {
        foreach(var controller in controllers)
        {
            controller.TextUpdate();
        }
    }
}

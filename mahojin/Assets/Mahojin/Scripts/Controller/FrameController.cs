using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour {
    private Selectable mySelectable;
    private Color defaultDisable;

	// Use this for initialization
	void Start () {
        mySelectable = GetComponent<Selectable>();
	}
	
    /// <summary>
    /// フレームを選択する
    /// </summary>
    public void Select()
    {
        var colors = mySelectable.colors;
        defaultDisable = colors.disabledColor;
        colors.disabledColor = colors.highlightedColor;
        mySelectable.colors = colors;
    }

    /// <summary>
    /// フレーム選択を解除する
    /// </summary>
    public void UnSelect()
    {
        var colors = mySelectable.colors;
        colors.disabledColor = defaultDisable;
        mySelectable.colors = colors;
    }
}

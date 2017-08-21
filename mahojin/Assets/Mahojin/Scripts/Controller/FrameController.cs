using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour {
    [SerializeField] private bool isSelectable = true;
    public bool IsSelectable { get { return isSelectable; } }
    [SerializeField] private int num;
    private PanelController havePanel;
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

    /// <summary>
    /// パネルをセットする
    /// </summary>
    /// <param name="panel">セットするパネル</param>
    public void SetPanel(PanelController panel)
    {
        havePanel = panel;
        isSelectable = false;
        num = 100;
    }
    
    /// <summary>
    /// セットされたパネルを外す
    /// </summary>
    public void ResetPanel()
    {
        havePanel = null;
        isSelectable = true;
        num = 0;
    }
}

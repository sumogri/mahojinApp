using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 問題選択UIのコントローラー
/// </summary>
public class PuzzleSelectableController : MonoBehaviour {
    private Button button;
    private Text text;
    private int questionNum;    //問題番号
    public int QuestionNum {
        get { return questionNum; }
        set {
            questionNum = value;
        }
    }
    public QuestionManager QManager { get; set; }
    public PuzzleSelectableManager PManager { get; set; }
    
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        text = gameObject.GetComponentInChildren<Text>();
        button.onClick.AddListener(() => QManager.LoadQuestion(questionNum));
        button.onClick.AddListener(() => PManager.TopUIRoot.SetActive(false));
    }

    public void SetInteractive(bool act)
    {
        if(button == null) button = gameObject.GetComponent<Button>();
        button.interactable = act;
    }

    public void Update()
    {
        text.text = questionNum.ToString();
    }
}

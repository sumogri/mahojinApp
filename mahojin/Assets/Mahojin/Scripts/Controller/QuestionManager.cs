using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    [SerializeField] private Transform canvas;
    [SerializeField] private int questionLength;
    [SerializeField] private PuzzleSelectableManager pManager;
    public PuzzleSelectableManager PManager { get { return pManager; } }
    public int QuestionLength { get { return questionLength; } }
    private Object nowQuestion;
    private int nowQuestionNo;
    public int NowQuestionNo { get { return nowQuestionNo; } }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeNextQuestion()
    {
        if (nowQuestion == null) return;
        Destroy(nowQuestion);
        nowQuestionNo++;
        nowQuestion = Instantiate(Resources.Load("Prefabs/Questions/Question" + nowQuestionNo),canvas);
        var qgame = nowQuestion as GameObject;
        qgame.GetComponent<QuestionController>().Manager = this;
    }

    public void LoadQuestion(int no)
    {
        nowQuestion = Instantiate(Resources.Load("Prefabs/Questions/Question" + no),canvas);
        nowQuestionNo = no;
        var qgame = nowQuestion as GameObject;
        qgame.GetComponent<QuestionController>().Manager = this;
    }

    public void DestroyNowQuestion()
    {
        Destroy(nowQuestion);
    }
}

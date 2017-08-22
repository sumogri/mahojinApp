using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    [SerializeField] private Transform canvas;
    [SerializeField] private int questionLength;
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
    }

    public void LoadQuestion(int no)
    {
        nowQuestion = Instantiate(Resources.Load("Prefabs/Questions/Question" + no),canvas);
        nowQuestionNo = no;
    }

    public void DestroyNowQuestion()
    {
        Destroy(nowQuestion);
    }
}

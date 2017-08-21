using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject TopUIRoot;
    [SerializeField] private GameObject questionSelectRoot;
    private Object nowQuestion;
    private int nowQuestionNo;
    private Button[] buttons;

	// Use this for initialization
	void Start () {
        buttons = questionSelectRoot.GetComponentsInChildren<Button>();
        foreach(var i in Enumerable.Range(0,buttons.Length-1))
        {
            buttons[i].onClick.AddListener(() => LoadQuestion(i));
            buttons[i].onClick.AddListener(() => TopUIRoot.SetActive(false));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeNextQuestion()
    {
        if (nowQuestion == null) return;
        Destroy(nowQuestion);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnswerButtonController : MonoBehaviour {
    private QuestionController questionController;
    private GameObject CorrectEff;
    private GameObject InCorrectEff;

	// Use this for initialization
	void Start () {
        questionController = GetComponentInParent<QuestionController>();
        CorrectEff = AnswerUIManager.I.CorrectEffect;
        InCorrectEff = AnswerUIManager.I.InCorrectEffect;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonDown()
    {
        AnswerUIManager.I.AnswerUIRoot.SetActive(true);
        if (questionController.IsCorrectAnswer())
        {
            CorrectEff.SetActive(true);
        }
        else
        {
            InCorrectEff.SetActive(true);
        }
    }
}

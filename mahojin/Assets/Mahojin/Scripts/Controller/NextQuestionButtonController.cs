using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuestionButtonController : MonoBehaviour {
    [SerializeField] private QuestionManager qManager;

	// Use this for initialization
	void Start () {
    }

    public void UpdateActive()
    {
        gameObject.SetActive(qManager.QuestionLength != qManager.NowQuestionNo + 1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

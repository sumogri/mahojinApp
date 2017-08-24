using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTopButtonController : MonoBehaviour {
    private QuestionController qController;
    
	// Use this for initialization
	void Start () {
        qController = GetComponentInParent<QuestionController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonDown()
    {
        qController.Manager.PManager.TopUIRoot.SetActive(true);
        qController.Manager.DestroyNowQuestion();
    }
}

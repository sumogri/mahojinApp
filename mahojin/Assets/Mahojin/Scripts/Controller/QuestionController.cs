using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mahojin;

public class QuestionController : MonoBehaviour {
    private FrameManager frameManager;
    public enum CorrectMode { MS3,MS4 }
    [SerializeField] private CorrectMode correctMode;

	// Use this for initialization
	void Start () {
        frameManager = GetComponentInChildren<FrameManager>();
        transform.SetAsFirstSibling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsCorrectAnswer()
    {
        bool ret = false;
        switch (correctMode)
        {
            case CorrectMode.MS3:
                ret = MS3Math.IsMagicSquare(frameManager.getCells());
                break;
            case CorrectMode.MS4:
                ret = MS4Math.IsMagicSquare(frameManager.getCells());
                break;
        }
        
        return ret;
    }
}

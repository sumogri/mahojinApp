using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleSelectableManager : MonoBehaviour {
    [SerializeField] private GameObject controllerRoot;
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private GameObject topUIRoot;
    public GameObject TopUIRoot { get { return topUIRoot; } set { topUIRoot = value; } }
    private PuzzleSelectableController[] controllers;

	// Use this for initialization
	void Start () {
        controllers = controllerRoot.GetComponentsInChildren<PuzzleSelectableController>();
        for(int i = 0; i <controllers.Length; i++)
        {
            controllers[i].QuestionNum = i;
            controllers[i].QManager = questionManager;
            controllers[i].PManager = this;
            controllers[i].SetInteractive(i < questionManager.QuestionLength);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

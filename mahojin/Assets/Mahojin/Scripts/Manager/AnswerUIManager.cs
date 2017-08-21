using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerUIManager : SingletonMonoBehaviour<AnswerUIManager> {
    [SerializeField] private GameObject correctEffect;
    public GameObject CorrectEffect { get { return correctEffect; } }
    [SerializeField] private GameObject inCorrectEffect;
    public GameObject InCorrectEffect { get { return inCorrectEffect; } }
    [SerializeField] private GameObject answerUIRoot;
    public GameObject AnswerUIRoot { get { return answerUIRoot; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

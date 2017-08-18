using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// インフォボックスのコントローラー
/// </summary>
public class InfoContoroller : MonoBehaviour {
    [SerializeField] private GameObject textsRootObj;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    private Text[] texts;
    private int nowViewId = 0;

	// Use this for initialization
	void Start () {
        texts = textsRootObj.GetComponentsInChildren<Text>(true);
        foreach (var text in texts) text.gameObject.SetActive(false);
        texts[nowViewId].gameObject.SetActive(true);
        backButton.onClick.AddListener(BackButtonOnClick);
        nextButton.onClick.AddListener(NextButtonOnclick);
        UpdateButtons();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackButtonOnClick()
    {
        texts[nowViewId].gameObject.SetActive(false);
        nowViewId = nowViewId == 0 ? 0 : nowViewId-1;
        texts[nowViewId].gameObject.SetActive(true);
        UpdateButtons();
    }
    public void NextButtonOnclick()
    {
        texts[nowViewId].gameObject.SetActive(false);
        nowViewId = nowViewId == texts.Length - 1 ? texts.Length - 1 : nowViewId + 1;
        texts[nowViewId].gameObject.SetActive(true);
        UpdateButtons();
    }
    private void UpdateButtons()
    {
        backButton.interactable = nowViewId != 0;
        nextButton.interactable = nowViewId != texts.Length - 1;
    }
    
}

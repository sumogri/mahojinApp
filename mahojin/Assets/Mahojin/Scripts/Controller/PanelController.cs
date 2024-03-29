﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// Puzzleモードでの数字パネルのコントローラー
/// </summary>
public class PanelController : MonoBehaviour {
    [SerializeField] FrameManager frameManager;
    [SerializeField] int num;
    public int Num { get { return num; } }
    private PanelManager manager;
    private FrameController toFrame;
    private Vector3 mouseDiff;

	// Use this for initialization
	void Start () {
		manager = transform.parent.GetComponent<PanelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDown()
    {
        mouseDiff = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
        if (toFrame != null)
        {
            toFrame.ResetPanel();
            toFrame.Select();
        }
    }

    public void OnDrag()
    {
        transform.position = Input.mousePosition + mouseDiff;

        //一番近いFrameがRadius以下の距離にあれば選択、過去のものは破棄
        FrameController to= frameManager.Frames.Where(x => x.IsSelectable)
                            .OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
        float distPos = Vector3.Distance(to.transform.position, transform.position);
        if (distPos < manager.Radius && to != toFrame)
        {
            to.Select();
            if (toFrame != null) toFrame.UnSelect();            
            toFrame = to;
        }
        else if(distPos >= manager.Radius)
        {
            if (toFrame != null) toFrame.UnSelect();
            toFrame = null;
        }
    }

    public void OnDrop()
    {
        if (toFrame != null)
        {
            transform.position = toFrame.transform.position;
            toFrame.UnSelect();
            toFrame.SetPanel(this);
        }
    }
}

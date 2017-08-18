using System.Linq;
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
    private Vector3 mouseDiff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDown()
    {
        mouseDiff = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
    }

    public void OnDrag()
    {
        transform.position = Input.mousePosition + mouseDiff;
    }

    public void OnDrop()
    {
        FrameController to = frameManager.Frames.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
        transform.position = to.transform.position;
    }
}

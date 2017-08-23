using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FrameManager : MonoBehaviour {
    public FrameController[] Frames { get; set; }
    
	// Use this for initialization
	void Start () {
        Frames = GetComponentsInChildren<FrameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int?[] getCells()
    {
        //選択可能 == 空欄とみなして、Frameをセル情報に変換
        return Frames.Select(x => (x.IsSelectable)? null : (int?) x.Num).ToArray();
    }
}

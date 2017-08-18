using System.Collections;
using System.Collections.Generic;
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
}

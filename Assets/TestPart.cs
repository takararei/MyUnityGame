using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPart : MonoBehaviour {
    public Transform t;
    public int index;

    public void OnClick()
    {
        //t.SetSiblingIndex(index);
        //t.SetAsFirstSibling();
        t.SetAsLastSibling();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

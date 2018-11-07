using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIPanel : BasePanel {

    //private CanvasGroup canvasGroup;
    // Use this for initialization
    public override void Start () {
        base.Start();
        //canvasGroup = GetComponent<CanvasGroup>();
	}
    public override void OnPause()
    {
        base.OnPause();
        //canvasGroup.blocksRaycasts = false;
    }
    // Update is called once per frame
    public override void Update () {
		
	}
}

using Assets.Framework.Excel.Test;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TestUIPanel : BasePanel {

    private Button mTestBtn;
    Text mTestTxt;
    public override void Init()
    {
        base.Init();
        mRootUI = UnityTool.FindChild(UICanvas, UIPanelName.TestUIPanel);
        SetCanvasGroup();
        mTestBtn = UITool.FindChild<Button>(mRootUI, "TestBtn");
        mTestTxt = UITool.FindChild<Text>(mRootUI, "TestTxt");
        mTestBtn.onClick.AddListener(TestClick);
        
    }
    

    private void TestClick()
    {
        Debug.Log("测试点击");
        //DoFadeShow(canvasGroup);
        //DoScaleHide(mRootUI.transform);
        //TestDataManager manager = Resources.Load<TestDataManager>("AssetData/TestData");
        //Debug.Log(manager.TestDataList[0].name);
        //manager.Test();
        //Debug.Log(mTestTxt.transform.position);
        //Debug.Log(Screen.height);
        //mTestTxt.transform.localPosition = new Vector3(0,0,0);
    }
    public override void Update () {
		
	}

    public override void OnShow()
    {
        //base.OnShow();
        DGTool.DoFadeShow(canvasGroup);
        //mRootUI.transform.DOLocalMoveX(,);

    }
}

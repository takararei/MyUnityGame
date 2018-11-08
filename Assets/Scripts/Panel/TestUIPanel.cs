using Assets.Framework.Excel.Test;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIPanel : BasePanel {

    private Button mTestBtn;
    public override void Init()
    {
        base.Init();
        mRootUI = UnityTool.FindChild(UICanvas, UIPanelName.TestUIPanel);
        mTestBtn = UITool.FindChild<Button>(mRootUI, "TestBtn");

        mTestBtn.onClick.AddListener(TestClick);
        
    }
    private void TestClick()
    {
        Debug.Log("测试点击");
        TestDataManager manager = Resources.Load<TestDataManager>("AssetData/TestData");
        //Debug.Log(manager.TestDataList[0].name);
        manager.Test();
    }
    public override void Update () {
		
	}
}

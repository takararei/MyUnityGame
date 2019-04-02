using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerSetCanvas : MonoBehaviour {
    public Transform TowerSet;
    public Transform TowerSelect;
    public Transform[] pos;
    public Transform Slow;
    public Transform Gun;
    public Transform Magic;
    public Transform Arrow;
    public Transform UpLevel;
    public Transform Sell;
    Button btn_Slow;
    Button btn_Gun;
    Button btn_Magic;
    Button btn_Arrow;
    Text slow_Price;
    Text gun_Price;
    Text magic_Price;
    Text arrow_Price;
    // Use this for initialization
    Vector3 initPos;
    private void Awake()
    {
        initPos = new Vector3(-900, 0, 0);
        btn_Slow = Slow.gameObject.GetComponent<Button>();
        btn_Gun = Gun.gameObject.GetComponent<Button>();
        btn_Arrow = Arrow.gameObject.GetComponent<Button>();
        btn_Magic = Magic.gameObject.GetComponent<Button>();
        //btn_Slow.onClick
        //查看当前的关卡允许建造的塔的类型
          //看一下是不是四种塔都允许建
          //实例四个建塔按钮出来
        //如果当前的位置没有塔
          //查看当前的关卡允许建造的塔的类型，进行关闭和开启
        //有塔
          //是否能升级，主要看下一级的塔是否在允许列表内，能就显示升级，不能就锁
    }
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(4))
        {

        }
    }

    void GunClick()
    {

    }

    void SlowClick()
    {

    }

    void ArrowClick()
    {

    }

    

    public void CorrectTowerSetCanvas()
    {
        GridPoint selectGrid = GameController.Instance.selectGrid;
        //transform.localPosition = selectGrid.transform.localPosition;
        transform.position= Camera.main.WorldToScreenPoint(selectGrid.transform.position);
        if (selectGrid.gridState.hasTower)
        {
            //显示升级
            TowerSet.gameObject.SetActive(true);
            TowerSelect.gameObject.SetActive(false);
            CorrectTowerSet(selectGrid.gridState.id);
        }
        else
        {
            //显示选择
            TowerSelect.gameObject.SetActive(true);
            TowerSet.gameObject.SetActive(false);
            CorrectTowerSelect(selectGrid.gridState.id);
        }
        
    }

    void CorrectTowerSet(int index)
    {
        if (index % 9 == 0)
        {
            UpLevel.position = pos[6].position;
            Sell.position = pos[5].position;
        }
        else if ((index + 1) % 9 == 0)
        {
            UpLevel.position = pos[5].position;
            Sell.position = pos[7].position;
        }
        else
        {
            UpLevel.position = pos[6].position;
            Sell.position = pos[7].position;
        }
    }

    void CorrectTowerSelect(int index)
    {
        if (index <= 7)
        {
            Slow.position = pos[6].position;
            Arrow.position = pos[7].position;
            Gun.position = pos[1].position;
            Magic.position = pos[2].position;
        }
        else if (index >= 100)
        {
            Gun.position = pos[6].position;
            Magic.position = pos[7].position;
            Slow.position = pos[0].position;
            Arrow.position = pos[3].position;
        }
        else if (index % 9 == 0)
        {
            Slow.position = pos[0].position;
            Arrow.position = pos[4].position;
            Gun.position = pos[1].position;
            Magic.position = pos[5].position;
        }
        else if ((index + 1) % 9 == 0)
        {
            Slow.position = pos[4].position;
            Arrow.position = pos[3].position;
            Gun.position = pos[5].position;
            Magic.position = pos[2].position;
        }
        else
        {
            Slow.position = pos[0].position;
            Arrow.position = pos[3].position;
            Gun.position = pos[1].position;
            Magic.position = pos[2].position;
        }
    }

    public void ResetCanvas()
    {
        transform.position = initPos;
    }
}

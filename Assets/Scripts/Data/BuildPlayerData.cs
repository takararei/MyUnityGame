using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlayerData : MonoBehaviour
{
    public PlayerData data;
    PlayerDataOperator po;

    private void Awake()
    {
        po = PlayerDataOperator.Instance;
        //po.Init();
        po.playerData = po.LoadPlayerData();
        data = PlayerDataOperator.Instance.playerData;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            po.playerData = data;
            po.SavePlayerData();
            Debug.Log("保存");
        }
    }
}

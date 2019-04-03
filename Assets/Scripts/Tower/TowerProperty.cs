using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class TowerProperty:MonoBehaviour
{
    public Transform target;
    private void Awake()
    {
        
    }
    
    private void Update()
    {
        if(target==null||GameController.Instance.isPause==false)
        {
            return;
        }

        transform.LookAt(target.position + new Vector3(0, 0, 2));

        if (transform.eulerAngles.y == 0)
        {
            transform.eulerAngles += new Vector3(0, 90, 0);
        }
    }

}
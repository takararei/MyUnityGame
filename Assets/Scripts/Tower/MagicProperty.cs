using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MagicProperty:TowerProperty//可抽出接口，主要是TransformRotate、attack、recycle方法
{
    
    protected override void TransformRotate()
    {

    }

    public override void GetBullectProperty(Bullect obj)
    {
        obj.transform.SendMessage("SetTrans", target);
    }
} 
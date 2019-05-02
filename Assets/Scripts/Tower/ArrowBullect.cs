using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ArrowBullect : Bullect
{
    protected override void BullectMove()
    {
        transform.position = Vector3.Lerp(transform.position, targetTrans.position,
              1 / Vector3.Distance(transform.position, targetTrans.position) * Time.deltaTime * moveSpeed * 10);
        transform.up = targetTrans.position - transform.position;
    }
}
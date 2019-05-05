using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ArrowBullect : Bullect
{
    protected override void BullectMove()
    {
        transform.position = Vector3.Lerp(transform.position, bsTower.towerProperty.target.position,
              1 / Vector3.Distance(transform.position, bsTower.towerProperty.target.position) * Time.deltaTime * moveSpeed * 10);
        transform.up = bsTower.towerProperty.target.position - transform.position;
    }
}
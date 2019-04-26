using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SlowProperty:TowerProperty
{
    protected override void TransformRotate()
    {
        //base.TransformRotate();
    }

    //protected override void Attack()
    //{
    //    //base.Attack();
    //    if (target == null)
    //    {
    //        return;
    //    }
    //    //animator.Play("Attack");
    //    isBeginCD = true;
    //    GameController.Instance.CreateBullect(baseTower);
    //}
}
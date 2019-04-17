using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseEnemy
{
    void EnemyMove();

    //重置资源时调用的
    void ResetEnemy();

    void TakeDamage(Bullect bullect);
    
}

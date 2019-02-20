using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework
{
    public class PlayerManager:Singleton<PlayerManager>
    {
        /*
         需存档的数据:
         1.钻石数
         2.持有的道具和数量
         3.每一关卡的星级和清空障碍
         4.每一关卡的成就
         5.当前的关卡数
         
         */

        public override void Init()
        {
            base.Init();
        }
    }
}

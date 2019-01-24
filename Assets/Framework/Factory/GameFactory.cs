using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework.Factory
{
    public class GameFactory : BaseFactory
    {
        public GameFactory()
        {
            loadPath += "Game/";
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework.Factory
{
    public class UIFactory : BaseFactory
    {
        public UIFactory()
        {
            loadPath += "UI/";
        }
    }

}

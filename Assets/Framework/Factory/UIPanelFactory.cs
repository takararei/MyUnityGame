using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework.Factory
{
    public class UIPanelFactory : BaseFactory
    {
        public UIPanelFactory()
        {
            loadPath += "UIPanel/";
        }
    }
}

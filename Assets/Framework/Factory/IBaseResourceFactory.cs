using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework.Factory
{
    public interface IBaseResourceFactory<T>
    {
        T GetResource(string resourcePath);
    }
}
 
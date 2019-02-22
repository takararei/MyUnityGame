using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework.UI
{
    public interface IBaseUIListItem
    {
        void Clear();
        int GetID();
        void SetID(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Netflix.Helpers.Dependency
{
    public interface IToast
    {
        void ShowToast(string message);
    }
}

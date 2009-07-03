using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elink
{
    public interface ITariff
    {
        Color AllbackColor { set; }
        bool IsFocused { get; }
        void SetCapture(EventHandler enter, EventHandler leave);
    }
}

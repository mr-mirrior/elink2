using System;
using System.Collections.Generic;
using System.Text;

namespace Elink
{
    public interface IBriefReport
    {
        double DailyConsumption { set; }
        double EstForTheYear { set; }
        double TotalForTheYear { set; }
    }
}

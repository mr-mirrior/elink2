using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elink
{
    public interface ITariffEmb
    {
        void SetData(Tariff t);
        void LoadPage();
        Tariff SaveData();
        void SaveToXML(string xml);
        void LoadFromXML(string xml);
        string TariffName { get; set; }
        DateTime UpdateTime { get; set; }
    }
}

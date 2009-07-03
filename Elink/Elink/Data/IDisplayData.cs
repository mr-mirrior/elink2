using System;
namespace Elink
{
    interface IDisplayData
    {
        void SetDay(DateTime day);
        void UpdateData();
        void SetMode(int mode); // 0~2, energy cost carbon
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCoreLib
{
    public class Analytics
    {
        public static List<DateTime> PopularMonths(List<DateTime> dates)
        {
            List<DateTime> resultDate = new List<DateTime>();

            int i = 0;

            foreach (var item in dates)
            {
                if (item == DateTime.MaxValue)
                {
                    resultDate.Insert(++i, item);
                }
            }

            return resultDate;
        }
    }
}

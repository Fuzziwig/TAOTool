using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAOTool
{
    public class Reading
    {
        public DateTime RDate { get; set; }
        public double Mwh { get; set; }
        public double M3 { get; set; }
        public int UserID { get; set; }
        public override string ToString()
        {
            return RDate.ToString() + " - " + Mwh.ToString()+ " Mwh - " + M3.ToString() + " M3";
        }
    }
}

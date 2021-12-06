using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1008team
{
    public class Team
    {
        public string Sname { get; set; }
        public DateTime Tstartdate { get; set; }
        public DateTime Tenddate { get; set; }
        public string Tname { get; set; }


        public Team(string sname, string tname, DateTime startdate, DateTime enddate)
        {
            Sname = sname;
            Tname = tname;
            Tstartdate = startdate;
            Tenddate = enddate;
        }
       /* public Team(string tname, DateTime startdate)
        {
            Tname = tname;
            Tstartdate = startdate;
        }*/
    }
}

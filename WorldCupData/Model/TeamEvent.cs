using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Model
{
    public class TeamEvent
    {
        public int Id { get; set; }
        public string TypeOfEvent { get; set; }
        public string Player { get; set; }
        public string Time { get; set; }
    }
}

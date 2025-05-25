using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Model
{
    public class GroupResult
    {
        public int Id { get; set; }
        public string Letter { get; set; }
        public List<TeamResult> OrderedTeams { get; set; }
    }
}

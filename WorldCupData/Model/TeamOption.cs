using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Model
{
    public class TeamOption
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{Name} ({Code})";
    }
}
